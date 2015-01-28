//-----------------------------------------------------------------------
// <copyright file="AccountRepository.cs" company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SD.CodeProblem.DevAssignment.Contracts.Data;
    using SD.CodeProblem.DevAssignment.Data.DataModel;
   
    /// <summary>
    /// Account database context repository implementation.
    /// </summary>
    public class AccountRepository : IDataRepository<Account>
    {
        /// <summary>
        /// Database context object.
        /// </summary>
        private readonly IAccountDbContext _context = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountRepository"/> class.
        /// </summary>
        /// <param name="context">Database context object injection.</param>
        public AccountRepository(IAccountDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Load list of entities.
        /// </summary>
        /// <param name="filters">Filter functions list.</param>
        /// <returns>Enumerable collection of Account records.</returns>
        public async Task<IEnumerable<Account>> Load(List<Func<IQueryable<Account>, IQueryable<Account>>> filters = null)
        {
            IQueryable<Account> query = _context.Account;

            if (filters == null)
            {
                return await query.ToListAsync();
            }

            foreach (var filter in filters)
            {
                query = filter(query);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Get data entity by identifier.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <returns>Entity object.</returns>
        public async Task<Account> GetById(int id)
        {
            return await _context.Account.FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Update exist record in the database.
        /// </summary>
        /// <param name="data">Entity data object to be updated.</param>
        /// <returns>Entity object of TData type.</returns>
        public Task<Account> Update(Account data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create new record in the database.
        /// </summary>
        /// <param name="data">Entity data object to be created.</param>
        /// <returns>Entity object of TData type.</returns>
        public Task<Account> Create(Account data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete record from database.
        /// </summary>
        /// <param name="data">Entity data type to be deleted.</param>
        /// <returns>Task for async operation.</returns>
        public Task Delete(Account data)
        {
            throw new NotImplementedException();
        }
    }
}
