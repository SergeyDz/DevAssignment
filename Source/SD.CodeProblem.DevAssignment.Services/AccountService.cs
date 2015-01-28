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
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SD.CodeProblem.DevAssignment.Contracts.Data;
    using SD.CodeProblem.DevAssignment.Contracts.Services;
    using SD.CodeProblem.DevAssignment.Data.DataModel;

    /// <summary>
    /// Account-related basic operations service.
    /// </summary>
    public class AccountService : IAccountService
    {
        /// <summary>
        /// Data repository object.
        /// </summary>
        private readonly IDataRepository<Account> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="repository">Data repository injection.</param>
        public AccountService(IDataRepository<Account> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get account amount calculated value by account id.
        /// </summary>
        /// <param name="accountId">Account identifier.</param>
        /// <returns>Returns account amount value.</returns>
        public async Task<double> GetAccountAmount(int accountId)
        {
            var account = await _repository.GetById(accountId);
            return account.Orders.Sum(p => p.Amount);
        }

        /// <summary>
        /// Get full list of entities.
        /// </summary>
        /// <param name="filters">Filter queribale functions list.</param>
        /// <typeparam name="T">entity type parameter.</typeparam>
        /// <returns>Generic collection of requested type.</returns>
        public async Task<IEnumerable<T>> GetList<T>(List<Func<IQueryable<T>, IQueryable<T>>> filters = null)
        {
            IEnumerable<Account> list = null;

            if (filters == null)
            {
                list = await _repository.Load();
            }
            else
            {
                list = await _repository.Load(filters.Cast<Func<IQueryable<Account>, IQueryable<Account>>>().ToList());
            }

            return list.Cast<T>();
        }
    }
}
