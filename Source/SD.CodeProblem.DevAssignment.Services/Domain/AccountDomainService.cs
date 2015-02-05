//-----------------------------------------------------------------------
// <copyright file=AccountDomainService company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.Services.Domain
{
    using AutoMapper;
    using SD.CodeProblem.DevAssignment.Contracts.Services.Data;
    using data = SD.CodeProblem.DevAssignment.Data.Model;
    using domain = SD.CodeProblem.DevAssignment.Domain.Model;

    /// <summary>
    /// Account Domain Service.
    /// </summary>
    public class AccountDomainService : GenericDomainService<data.Account, domain.Account>
    {
        /// <summary>
        /// Initializes an instance of the <see cref="AccountDomainService"/> class.
        /// </summary>
        /// <param name="repository">Repository class instance.</param>
        /// <param name="mappingEngine">Maooing engine class instance for convert data model entity to domain model entity.</param>
        public AccountDomainService(IDataRepository<data.Account> repository, MappingEngine mappingEngine) : base(repository, mappingEngine)
        {
        }
    }
}
