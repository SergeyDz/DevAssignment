//-----------------------------------------------------------------------
// <copyright file=GenericDataRepository company="SD">
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
    using SD.CodeProblem.DevAssignment.Contracts.Common;
    using SD.CodeProblem.DevAssignment.Contracts.Services.Data;

    /// <summary>
    /// Generic data rpository class to working with EDMX context.
    /// </summary>
    /// <typeparam name="TData">Data type parameter.</typeparam>
    /// <typeparam name="TDbContext">Database context type parameter.</typeparam>
    public abstract class GenericDataRepository<TData, TDbContext> : IDataRepository<TData> where TData : class, IIdentity<int>, new() 
        where TDbContext : DbContext
    {
        /// <summary>
        /// Database context storage.
        /// </summary>
        protected readonly DbContext Context;

        /// <summary>
        /// Initializes an instance of the <see cref="GenericDataRepository"/> class.
        /// </summary>
        /// <param name="context">Database context reference.</param>
        protected GenericDataRepository(TDbContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Abstract load data method.
        /// </summary>
        /// <param name="filters">Filter function expression.</param>
        /// <returns>Returns list of TData entities.</returns>
        public abstract Task<IEnumerable<TData>> Load(List<Func<IQueryable<TData>, IQueryable<TData>>> filters = null);

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id">Integer identifier.</param>
        /// <returns>TData instance.</returns>
        public async Task<TData> GetById(int id)
        {
            return await Context.Set<TData>().FindAsync(id);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="data">Entity instance.</param>
        /// <returns>Returns updated entity from database context.</returns>
        public async Task<TData> Update(TData data)
        {
            Context.Entry(data).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return data;
        }

        /// <summary>
        /// Create new entity.
        /// </summary>
        /// <param name="data">Entity instance.</param>
        /// <returns>Returns instance of created entity with Id.</returns>
        public async Task<TData> Create(TData data)
        {
            Context.Entry(data).State = EntityState.Added;
            await Context.SaveChangesAsync();
            return data;
        }

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <returns>Task for await.</returns>
        public async Task Delete(int id)
        {
            TData entity = new TData() { Id = id };
            Context.Entry(entity).State = EntityState.Deleted;
            await Context.SaveChangesAsync();
        }
    }
}
