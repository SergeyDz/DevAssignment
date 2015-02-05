﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using SD.CodeProblem.DevAssignment.Business.Model;
using SD.CodeProblem.DevAssignment.Contracts.Services.Domain;
using SD.CodeProblem.DevAssignment.Data.Model;
using SD.CodeProblem.DevAssignment.Services;
using SD.CodeProblem.DevAssignment.Services.Data;
using SD.CodeProblem.DevAssignment.Services.Domain;
using SD.CodeProblem.DevAssignment.Services.Mapping;
using Account = SD.CodeProblem.DevAssignment.Domain.Model.Account;

namespace DevAssignment.WebApi.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private IDomainService<Account> _domainService;
       

        public AccountController()
        {
            _domainService = new AccountDomainService(new AccountDataRepository(new AccountDbContext()), new MappingEngine(new GenericMapperConfigurationProvider<AccountListMappingProfile>()));
        }

        [Route("{accountId}/amount")]
        public async Task<double> GetAccountAmountAsync(int accountId)
        {
            AccountInfo info = new AccountInfo(accountId, new AccountService(_domainService));
            await info.RefreshAmount();
            return info.Amount;
        }

        [Route("")]
        public async Task<List<Account>> GetAccountsAsync()
        {
            var result = await _domainService.Load();
            return result.ToList();
        }
    }
}
