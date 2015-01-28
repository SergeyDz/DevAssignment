//-----------------------------------------------------------------------
// <copyright file="AccountInfo.cs" company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.Business.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using SD.CodeProblem.DevAssignment.Contracts.Services;

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
        /// Thread safe local amount variable.
        /// </summary>
        private ThreadLocal<double> _amount;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountInfo"/> class.
        /// </summary>
        /// <param name="accountId">Account identifier.</param>
        /// <param name="accountService">Account Service provider.</param>
        public AccountInfo(int accountId, IAccountService accountService)
        {
            _accountId = accountId;
            _accountService = accountService;
            _amount = new ThreadLocal<double>();
        }

        /// <summary>
        /// Gets amount value for current account instance.
        /// </summary>
        /// <remarks>Amount will contains only data snapshot, made during last RefreshAmount call.</remarks>
        public double Amount
        {
            get { return _amount.Value; }
            private set { _amount.Value = value; }
        }

        /// <summary>
        /// Refresh account amount value by instance member account id.
        /// </summary>
        /// <returns>Task for sync context.</returns>
        public async Task RefreshAmount()
        {
            Amount = await _accountService.GetAccountAmount(_accountId);
        }
    }
}
