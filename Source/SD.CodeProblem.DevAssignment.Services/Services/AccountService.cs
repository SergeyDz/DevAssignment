//-----------------------------------------------------------------------
// <copyright file="AccountService.cs" company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.Services.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using SD.CodeProblem.DevAssignment.Contracts.Services;
    using SD.CodeProblem.DevAssignment.Contracts.Services.Domain;
    using SD.CodeProblem.DevAssignment.Services.Data;
    using SD.CodeProblem.DevAssignment.Services.Domain;
    using SD.CodeProblem.DevAssignment.Services.Mapping;
    using data = SD.CodeProblem.DevAssignment.Data.Model;
    using domain = SD.CodeProblem.DevAssignment.Domain.Model;

    /// <summary>
    /// Account-related basic operations service.
    /// </summary>
    public class AccountService : IAccountService
    {
        /// <summary>
        /// Data repository object.
        /// </summary>
        private readonly IDomainService<domain.Account> _domainService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="domainService">Domain service reference.</param>
        public AccountService(IDomainService<domain.Account> domainService)
        {
            _domainService = new GenericDomainService<data.Account, domain.Account>(
                new AccountDataRepository(data.AccountDbContext.Create("AccountDbHarbor")),
                new MappingEngine(new GenericMapperConfigurationProvider<AccountMappingProfile>()));
        }

        /// <summary>
        /// Get account amount calculated value by account id.
        /// </summary>
        /// <param name="accountId">Account identifier.</param>
        /// <returns>Returns account amount value.</returns>
        public async Task<double> GetAccountAmount(int accountId)
        {
            var account = await _domainService.GetById(accountId);
            return account.Orders.Sum(o => o.Amount);
        }
    }
}
