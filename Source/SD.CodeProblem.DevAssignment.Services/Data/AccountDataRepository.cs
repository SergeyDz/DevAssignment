//-----------------------------------------------------------------------
// <copyright file=AccountDataRepository company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SD.CodeProblem.DevAssignment.Contracts.Services.Data;
    using SD.CodeProblem.DevAssignment.Data.Model;

    /// <summary>
    /// Strong type repository class to load Accounts entities
    /// </summary>
    public class AccountDataRepository : GenericDataRepository<Account, AccountDbContext>
    {
        /// <summary>
        /// Initializes an instance of the <see cref="AccountDataRepository"/> class.
        /// </summary>
        /// <param name="context">Database context.</param>
        public AccountDataRepository(AccountDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Load list of entities from Database provider.
        /// </summary>
        /// <param name="filters">Filter function definition.</param>
        /// <returns>Returns list of Account entities.</returns>
        public async override Task<IEnumerable<Account>> Load(List<Func<IQueryable<Account>, IQueryable<Account>>> filters = null)
        {
            if (filters != null)
            {
                throw new NotImplementedException("Filtering is not implemented for AccountDataRepository.Load() method.");
            }

            var accountContext = Context as AccountDbContext;
            return await accountContext.Account.ToListAsync();
        }
    }
}
