//-----------------------------------------------------------------------
// <copyright file="IDomainService.cs" company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------

namespace SD.CodeProblem.DevAssignment.Contracts.Services.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SD.CodeProblem.DevAssignment.Contracts.Common;

    /// <summary>
    /// Service contract for Domain Service entity
    /// </summary>
    /// <typeparam name="TDomain">Domain model type.</typeparam>
    public interface IDomainService<TDomain> where TDomain : IIdentity<int>
    {
        /// <summary>
        /// Get data entity by identifier.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <returns>Entity object.</returns>
        Task<TDomain> GetById(int id);

        /// <summary>
        /// Update exist record in the database.
        /// </summary>
        /// <param name="model">Entity data object to be updated.</param>
        /// <returns>Entity object of TData type.</returns>
        Task<TDomain> Update(TDomain model);

        /// <summary>
        /// Create new record in the database.
        /// </summary>
        /// <param name="model">Entity data object to be created.</param>
        /// <returns>Entity object of TData type.</returns>
        Task<TDomain> Create(TDomain model);

        /// <summary>
        /// Delete record from database.
        /// </summary>
        /// <param name="id">Identity of deletable record.</param>
        /// <returns>Task for async operation.</returns>
        Task Delete(int id);

        /// <summary>
        /// Create new record in the database.
        /// </summary>
        /// <returns>Collection of entity objects of TData type.</returns>
        Task<IEnumerable<TDomain>> Load();
    }
}
