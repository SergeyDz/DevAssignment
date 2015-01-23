//-----------------------------------------------------------------------
// <copyright file="IAccountService.cs" company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Account service interface definition.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Get account amount by account id.
        /// </summary>
        /// <param name="accountId">Account identifier.</param>
        /// <returns>Returns account amount value.</returns>
        Task<double> GetAccountAmount(int accountId);
    }
}
