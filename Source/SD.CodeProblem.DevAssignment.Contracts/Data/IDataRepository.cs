//-----------------------------------------------------------------------
// <copyright file="IDataRepository.cs" company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.Contracts.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for Data Repository.
    /// </summary>
    /// <typeparam name="TData">Type parameter for data model entity.</typeparam>
    public interface IDataRepository<TData>
    {
        /// <summary>
        /// Load list of entities.
        /// </summary>
        /// <param name="filters">Filter queribale functions list.</param>
        /// <returns>Enumerable collection of Account records.</returns>
        Task<IEnumerable<TData>> Load(List<Func<IQueryable<TData>, IQueryable<TData>>> filters = null);

        /// <summary>
        /// Get data entity by identifier.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <returns>Entity object.</returns>
        Task<TData> GetById(int id);

        /// <summary>
        /// Update exist record in the database.
        /// </summary>
        /// <param name="data">Entity data object to be updated.</param>
        /// <returns>Entity object of TData type.</returns>
        Task<TData> Update(TData data);

        /// <summary>
        /// Create new record in the database.
        /// </summary>
        /// <param name="data">Entity data object to be created.</param>
        /// <returns>Entity object of TData type.</returns>
        Task<TData> Create(TData data);

        /// <summary>
        /// Delete record from database.
        /// </summary>
        /// <param name="data">Entity data type to be deleted.</param>
        /// <returns>Task for async operation.</returns>
        Task Delete(TData data);
    }
}
