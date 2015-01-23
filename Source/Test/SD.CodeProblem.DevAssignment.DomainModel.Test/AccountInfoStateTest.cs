//-----------------------------------------------------------------------
// <copyright file="AccountInfoStateTest.cs" company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.DomainModel.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using Moq;
    using SD.CodeProblem.DevAssignment.Contracts;

    [TestFixture]
    public class AccountInfoStateTest
    {
        private int _accountId;
        private IAccountService _accountService;
        private double _amount;

        [SetUp]
        public void SetUp()
        {
            _accountId = 42;
            _amount = 236.45;

            Mock<IAccountService> accountServiceStub = new Mock<IAccountService>();
            accountServiceStub.Setup(m => m.GetAccountAmount(It.Is<int>(account => account <= 0)))
                .Callback(() => Task.Delay(10))
                .ThrowsAsync(new ArgumentOutOfRangeException());
            accountServiceStub.Setup(m => m.GetAccountAmount(It.IsInRange(1, int.MaxValue, Range.Inclusive)))
                .Callback(() => Task.Delay(10))
                .Returns<double>(t => Task.FromResult(_amount));

            _accountService = accountServiceStub.Object;
        }

        [Test(Description = "Test ckecks if Amount property will read before RefreshAmount() method call. Expected to return 0.")]
        public void Amount_RefreshAmountNotCalled_ReturnsZero()
        { 
            AccountInfo account = new AccountInfo(_accountId, _accountService);

            Assert.AreEqual(0, account.Amount);
        }

        [Test(Description = "Test checks if IAccountService is not initialized, but class will not faults while RefreshAmount() method not called.")]
        public void Amount_IAccountServiceNotDefined_ReturnsZero()
        {
            AccountInfo account = new AccountInfo(_accountId, null);

            Assert.AreEqual(0, account.Amount);
        }

        [Test(Description = "Test regular scenario when RefreshAmount called and Amount property requested.")]
        public async void RefreshAmount_PositiveAmountExists_ReturnaAccountValue()
        {
            AccountInfo account = new AccountInfo(_accountId, _accountService);
            await account.RefreshAmount();

            Assert.AreEqual(_amount, account.Amount);
        }

        [Test(Description = "Test scenario when RefreshAmount called few times, and then Amount property requested.")]
        public async void RefreshAmount_RefreshAmountDoubleCallWithSameData_ReturnaAccountValue()
        {
            AccountInfo account = new AccountInfo(_accountId, _accountService);
            await account.RefreshAmount();

            Assert.AreEqual(_amount, account.Amount);
            
            await account.RefreshAmount();

            Assert.AreEqual(_amount, account.Amount);
        }

        [TestCase(0)]
        [TestCase(double.MaxValue)]
        [TestCase(double.MinValue)]
        [TestCase(double.NaN)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity)]
        public async void RefreshAmount_RefreshAmountReturnsExtremalValues_ReturnsAccountValue(double amountValue)
        {
            _amount = amountValue;

            AccountInfo account = new AccountInfo(_accountId, _accountService);
            await account.RefreshAmount();

            Assert.AreEqual(_amount, account.Amount);
        }

        [Test(Description = "Test scenario when requested account number is invalid, and data was not found.")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task RefreshAmount_RefreshAmountForNotExistedAccountNumber_Exception()
        {
            _accountId = 0; 

            AccountInfo account = new AccountInfo(_accountId, _accountService);
            await account.RefreshAmount();
        }

        [Test(Description = "Test scenario when RefreshAmount called few times with data changes between calls. Amount property checked few times.")]
        public async void RefreshAmount_RefreshAmountDoubleCallWithDifferentData_ReturnaAccountValue()
        {
            AccountInfo account = new AccountInfo(_accountId, _accountService);
            await account.RefreshAmount();

            Assert.AreEqual(_amount, account.Amount);

            // change amount expectation between the calls.
            _amount = _amount / _accountId;

            await account.RefreshAmount();

            Assert.AreEqual(_amount, account.Amount);
        }

        [TearDown]
        public void TearDown()
        {
            _accountId = 0;
            _amount = 0.0;
            _accountService = null;
        }
    }
}
