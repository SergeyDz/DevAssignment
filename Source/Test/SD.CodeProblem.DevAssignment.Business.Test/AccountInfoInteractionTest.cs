//-----------------------------------------------------------------------
// <copyright file="AccountInfoInteractionTest.cs" company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.Business.Testt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using SD.CodeProblem.DevAssignment.Contracts.Services;
    using SD.CodeProblem.DevAssignment.Business.Model;

    [TestFixture(Category = "Business")]
    public class AccountInfoInteractionTest
    {
        [Test(Description = "Checks if IAccountService method was called during interaction with AccountInfo.")]
        public void RefreshAccount_AccountServiceCalledCheck_OnceCall()
        {
            Mock<IAccountService> mock = new Mock<IAccountService>();
            mock.Setup(m => m.GetAccountAmount(It.IsAny<int>()));

            AccountInfo account = new AccountInfo(4, mock.Object);
            account.RefreshAmount();
            var amount = account.Amount;

            mock.Verify(m=>m.GetAccountAmount(It.IsAny<int>()), Times.Once);
        }

        [Test(Description = "Checks if IAccountService method was called during interaction with AccountInfo.")]
        public void RefreshAccount_AccountServiceNotCalledCheck_NeverCall()
        {
            Mock<IAccountService> mock = new Mock<IAccountService>();
            mock.Setup(m => m.GetAccountAmount(It.IsAny<int>()));

            AccountInfo account = new AccountInfo(4, mock.Object);
            var amount = account.Amount;

            mock.Verify(m => m.GetAccountAmount(It.IsAny<int>()), Times.Never);
        }

        [Test(Description = "Check if accountId from AccountInfo instance was passed correctly to service.")]
        public void RefreshAccoun_CheckIfAccountIdParamPassCorrect_OnceCall()
        {
            int accountId = 5;
            Mock<IAccountService> mock = new Mock<IAccountService>();
            mock.Setup(m => m.GetAccountAmount(It.IsAny<int>()));

            AccountInfo account = new AccountInfo(accountId, mock.Object);
            account.RefreshAmount();
            var amount = account.Amount;

            mock.Verify(m => m.GetAccountAmount(It.Is<int>(a => a == accountId)), Times.Once);
        }
    }
}
