using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using SD.CodeProblem.DevAssignment.Contracts.Data;
using SD.CodeProblem.DevAssignment.Contracts.Services;
using SD.CodeProblem.DevAssignment.Data.DataModel;
using SD.CodeProblem.DevAssignment.Data.Repository;
using SD.CodeProblem.DevAssignment.DomainModel;
using SD.CodeProblem.DevAssignment.Services;

namespace DevAssignment.WebApi.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private IAccountService _accountService;
        private IDataRepository<Account> _repository;

        public AccountController()
        {
            _repository = new AccountRepository(new AccountDbContext());
            _accountService = new AccountService(_repository);
        }

        [Route("{accountId}/amount")]
        public async Task<double> GetAccountAmountAsync(int accountId)
        {
            return await _accountService.GetAccountAmount(accountId);
        }

        [Route("")]
        public async Task<List<Account>> GetAccountsAsync()
        {
            var filters = new List<Func<IQueryable<Account>, IQueryable<Account>>>();
            filters.Add(q => q.Include(p => p.CreatedBy));
            var result = await _accountService.GetList<Account>(filters);
            return result.ToList();
        }
    }
}
