//-----------------------------------------------------------------------
// <copyright file="AccountInfo.cs" company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SD.CodeProblem.DevAssignment.Contracts;

    /// <summary>
    /// Account information domain model member.
    /// </summary>
    public class AccountInfo
    {
        /// <summary>
        /// Account identifier number.
        /// </summary>
        private readonly int _accountId;

        /// <summary>
        ///  Field for IAccountService instance reference.
        /// </summary>
        private readonly IAccountService _accountService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountInfo"/> class.
        /// </summary>
        /// <param name="accountId">Account identifier.</param>
        /// <param name="accountService">Account Service provider.</param>
        public AccountInfo(int accountId, IAccountService accountService)
        {
            _accountId = accountId;
            _accountService = accountService;
        }

        /// <summary>
        /// Gets amount value for current account instance.
        /// </summary>
        /// <remarks>Amount will contains only snapshot of amount, made during last RefreshAmount call.</remarks>
        public double Amount { get; private set; }

        /// <summary>
        /// Refresh account amount value by instance member account id.
        /// </summary>
        public void RefreshAmount()
        {
            Amount = _accountService.GetAccountAmount(_accountId);
        }
    }
}
