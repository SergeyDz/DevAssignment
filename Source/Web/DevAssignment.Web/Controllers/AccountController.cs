using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using SD.CodeProblem.DevAssignment.Business.Model;
using SD.CodeProblem.DevAssignment.Contracts.Services.Domain;
using SD.CodeProblem.DevAssignment.Data.Model;
using SD.CodeProblem.DevAssignment.Services.Data;
using SD.CodeProblem.DevAssignment.Services.Domain;
using SD.CodeProblem.DevAssignment.Services.Mapping;
using SD.CodeProblem.DevAssignment.Services.Services;
using Account = SD.CodeProblem.DevAssignment.Domain.Model.Account;

namespace DevAssignment.WebApi.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private IDomainService<Account> _domainService;
       

        public AccountController()
        {
            _domainService = new AccountDomainService(new AccountDataRepository(AccountDbContext.Create("SQLSERVER_CONNECTION_STRING")), new MappingEngine(new GenericMapperConfigurationProvider<AccountListMappingProfile>()));
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
