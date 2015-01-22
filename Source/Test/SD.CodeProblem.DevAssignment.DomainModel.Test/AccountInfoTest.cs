//-----------------------------------------------------------------------
// <copyright file="AccountInfoTest.cs" company="SD">
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
    public class AccountInfoTest
    {
        private int _accountId;
        private IAccountService _accountService;
        private double _amount;

        [SetUp]
        public void SetUp()
        {
            _accountId = 42;
            _amount = 236.45;

            Mock<IAccountService> accountServiceMock = new Mock<IAccountService>();
            accountServiceMock.Setup(m => m.GetAccountAmount(It.Is<int>(account => account <= 0)))
                .Throws<ArgumentOutOfRangeException>();
            accountServiceMock.Setup(m => m.GetAccountAmount(It.IsInRange(1, int.MaxValue, Range.Inclusive)))
                .Returns<double>(val => _amount);

            _accountService = accountServiceMock.Object;
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
        public void RefreshAmount_PositiveAmountExists_ReturnaAccountValue()
        {
            AccountInfo account = new AccountInfo(_accountId, _accountService);
            account.RefreshAmount();

            Assert.AreEqual(_amount, account.Amount);
        }

        [Test(Description = "Test regular scenario when RefreshAmount called and Amount property requested, and Amount is negative.")]
        public void RefreshAmount_NegativeAmountExists_ReturnaAccountValue()
        {
            _amount = -_amount;

            AccountInfo account = new AccountInfo(_accountId, _accountService);
            account.RefreshAmount();

            Assert.AreEqual(_amount, account.Amount);
        }

        [Test(Description = "Test scenario when RefreshAmount called few times with same data, and then Amount property requested.")]
        public void RefreshAmount_RefreshAmountDoubleCallWithSameData_ReturnaAccountValue()
        {
            AccountInfo account = new AccountInfo(_accountId, _accountService);
            account.RefreshAmount();
            account.RefreshAmount();

            Assert.AreEqual(_amount, account.Amount);
        }

        [TestCase(0)]
        [TestCase(double.MaxValue)]
        [TestCase(double.MinValue)]
        public void RefreshAmount_RefreshAmountReturnsExtremalValues_ReturnsAccountValue(double amountValue)
        {
            _amount = amountValue;

            AccountInfo account = new AccountInfo(_accountId, _accountService);
            account.RefreshAmount();

            Assert.AreEqual(_amount, account.Amount);
        }

        [Test(Description = "Test scenario when requested account number is invalid, and data was not found.")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RefreshAmount_RefreshAmountForNotExistedAccountNumber_Exception()
        {
            _accountId = 0; 

            AccountInfo account = new AccountInfo(_accountId, _accountService);
            account.RefreshAmount();
        }

        [Test(Description = "Test scenario when RefreshAmount called few times with data changes between calls. Amount property checked few times.")]
        public void RefreshAmount_RefreshAmountDoubleCallWithDifferentData_ReturnaAccountValue()
        {
            AccountInfo account = new AccountInfo(_accountId, _accountService);
            account.RefreshAmount();

            Assert.AreEqual(_amount, account.Amount);

            _amount = _amount / _accountId;

            account.RefreshAmount();

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
