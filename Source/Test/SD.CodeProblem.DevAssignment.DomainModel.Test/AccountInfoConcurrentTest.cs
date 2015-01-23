//-----------------------------------------------------------------------
// <copyright file="AccountInfoConcurrentTest.cs" company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.DomainModel.Tes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Threading;
    using Moq;
    using NUnit.Framework;
    using SD.CodeProblem.DevAssignment.Contracts;

    [TestFixture]
    public class AccountInfoConcurrentTest
    {
        private double _amount;

        /// <summary>
        /// Test describes the problem, when few Tasks start to work with AccountInfo instance parallel. 
        /// The correct bahiour is when AccountInfo instance in one thread make snapshot of Amount value just after 
        /// RefreshAmount() call and keep in unchanged (isolated), no matter was called RefreshAmount() from parallel thread or not.
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
    }
}
