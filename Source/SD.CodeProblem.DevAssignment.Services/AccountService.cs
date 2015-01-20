//-----------------------------------------------------------------------
// <copyright file="AccountService.cs" company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SD.CodeProblem.DevAssignment.Contracts;

    /// <summary>
    /// Account-related basic operations service.
    /// </summary>
    public class AccountService : IAccountService
    {
        /// <summary>
        /// Get account amount calculated value by account id.
        /// </summary>
        /// <param name="accountId">Account identifier.</param>
        /// <returns>Returns account amount value.</returns>
        public double GetAccountAmount(int accountId)
        {
            throw new NotImplementedException();
        }
    }
}
