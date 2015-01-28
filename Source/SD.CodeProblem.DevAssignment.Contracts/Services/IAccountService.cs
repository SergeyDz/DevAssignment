//-----------------------------------------------------------------------
// <copyright file="IAccountService.cs" company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.Contracts.Services
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
        
        /// <summary>
        /// Get list of entities
        /// </summary>
        /// <param name="filters">Filter queribale functions list.</param>
        /// <typeparam name="T">Entity type to be get.</typeparam>
        /// <returns>Enumerable collection of fetch elements.</returns>
        Task<IEnumerable<T>> GetList<T>(List<Func<IQueryable<T>, IQueryable<T>>> filters = null);
    }
}
