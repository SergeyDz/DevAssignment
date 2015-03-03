//-----------------------------------------------------------------------
// <copyright file="AccountInfoConcurrentTest.cs" company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.Business.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Threading;
    using Moq;
    using NUnit.Framework;
    using SD.CodeProblem.DevAssignment.Contracts.Services;
    using SD.CodeProblem.DevAssignment.Business.Model;

    [TestFixture(Category = "TPL")]
    public class AccountInfoConcurrentTest
    {
        private double _amount;

        /// <summary>
        /// Test describes the problem, when few Tasks start to work with same AccountInfo instance parallel. 
        /// The correct bahiour is: when AccountInfo instance in one thread make snapshot of Amount value (just after 
        /// RefreshAmount() call ends) and keep in unchanged (isolated), no matter when was called RefreshAmount() 
        /// from parallel thread or not called at all.
        /// </summary>
        [Test(Description = "Check that per thread Amount snapshot stay unchanged, when other thread calls RefreshAmount()")]
        [Ignore("Long-running test. Need to be called per-request manually")]
        public async void RefreshAccount_ConcurrentCalls_ReturnsIsolatedAmountPerThread()
        {
            Mock<IAccountService> accountServiceStub = new Mock<IAccountService>();
            accountServiceStub.Setup(m => m.GetAccountAmount(It.IsInRange(1, int.MaxValue, Range.Inclusive)))
                .Callback(() => Thread.Sleep(10))
                .Returns<double>(t => Task.FromResult(_amount));

            AccountInfo info = new AccountInfo(43, accountServiceStub.Object);

            ManualResetEventSlim resetEventSlim1 = new ManualResetEventSlim();
            ManualResetEventSlim resetEventSlim2 = new ManualResetEventSlim();

            var task1 = Task.Run(async () =>
            {
                var amount = _amount = 17.45;
                await info.RefreshAmount();
                resetEventSlim2.Set();
                resetEventSlim1.Wait();
                Assert.AreEqual(amount, info.Amount);
            });

            var task2 = Task.Run(async () =>
            {
                resetEventSlim2.Wait();
                var amount = _amount = -34.56;
                await info.RefreshAmount();
                Assert.AreEqual(amount, info.Amount);
                resetEventSlim1.Set();
            });

            var task3 = Task.Run(() =>
            {
                Assert.AreEqual(0, info.Amount);
            });

            await Task.WhenAll(task1, task2, task3);
        }

        [Test(Description = "Checks how many times IAccountService method was called during interaction with AccountInfo.")]
        [Ignore("Long-running test. Need to be called per-request manually")]
        public async void RefreshAccount_AccountServiceCalledCheck_OnceCall()
        {
            _amount = 456.43;
            const int N = 10;

            Mock<IAccountService> mock = new Mock<IAccountService>();
            mock.Setup(m => m.GetAccountAmount(It.IsAny<int>()))
                .Callback(() => Thread.Sleep(100))
                .Returns<double>(t => Task.FromResult(_amount));

            AccountInfo account = new AccountInfo(4, mock.Object);
            Task[] tasks = new Task[N];
            for (int i = 0; i < N; i++)
            {
                tasks[i] = Task.Run(async () =>
                {
                    await account.RefreshAmount();
                    Assert.AreEqual(_amount, account.Amount);
                });
            }

            await Task.WhenAll(tasks);

            mock.Verify(m=>m.GetAccountAmount(It.IsAny<int>()), Times.Exactly(N));
        }
    }
}
