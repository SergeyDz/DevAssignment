//-----------------------------------------------------------------------
// <copyright file=GenericDomainService company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.Services.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using SD.CodeProblem.DevAssignment.Contracts.Common;
    using SD.CodeProblem.DevAssignment.Contracts.Services.Data;
    using SD.CodeProblem.DevAssignment.Contracts.Services.Domain;

    /// <summary>
    /// Generic cascade for domaon service.
    /// </summary>
    /// <typeparam name="TData">Data model entity type.</typeparam>
    /// <typeparam name="TDomain">Domai model entity type.</typeparam>
    /// <remarks>TData and TDomain types should be convertable to each other using MappingEbgine.</remarks>
    public class GenericDomainService<TData, TDomain> : IDomainService<TDomain> where TData : IIdentity<int> where TDomain : IIdentity<int>
    {
        /// <summary>
        /// Database repository reference.
        /// </summary>
        private readonly IDataRepository<TData> _repository;

        /// <summary>
        /// Mapping engine reference.
        /// </summary>
        private readonly MappingEngine _mapperEngine;
  
        /// <summary>
        /// Initializes an instance of the <see cref="GenericDomainService"/> class.
        /// </summary>
        /// <param name="dataRepository">Database repository instance.</param>
        /// <param name="mappingEngine">Mapping engine isntance.</param>
        public GenericDomainService(IDataRepository<TData> dataRepository, MappingEngine mappingEngine)
        {
            _repository = dataRepository;
            _mapperEngine = mappingEngine;
        }

        /// <summary>
        /// Get domain model instance by identifier.
        /// </summary>
        /// <param name="id">Identifier key.</param>
        /// <returns>Domain model instance.</returns>
        public async Task<TDomain> GetById(int id)
        {
            var dataModel = await _repository.GetById(id);
            return _mapperEngine.Map<TData, TDomain>(dataModel);
        }

        /// <summary>
        /// Update domain model instance.
        /// </summary>
        /// <param name="model">Domain model instance.</param>
        /// <returns>Returns saved domain model instance.</returns>
        public async Task<TDomain> Update(TDomain model)
        {
            var dataModel = _mapperEngine.Map<TDomain, TData>(model);
            dataModel = await _repository.Update(dataModel);

            return _mapperEngine.Map<TData, TDomain>(dataModel);
        }

        /// <summary>
        /// Craete new entity.
        /// </summary>
        /// <param name="model">Entity domain model instance.</param>
        /// <returns>Newly created entity model instance.</returns>
        public async Task<TDomain> Create(TDomain model)
        {
            var dataModel = _mapperEngine.Map<TDomain, TData>(model);
            dataModel = await _repository.Create(dataModel);

            return _mapperEngine.Map<TData, TDomain>(dataModel);
        }

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <returns>Returns task for await.</returns>
        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        /// <summary>
        /// Load entities collection.
        /// </summary>
        /// <returns>Returns enumerable collection.</returns>
        public async Task<IEnumerable<TDomain>> Load()
        {
            var list = await _repository.Load();
            return _mapperEngine.Map<List<TData>, List<TDomain>>(list.ToList());
        }
    }
}
