using AssessmentProject.Core;
using AssessmentProject.Core.DTOs.Account.Request;
using AssessmentProject.Core.DTOs.Account.Response;
using AssessmentProject.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssessmentProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AccountResponse>>> GetAccounts()
        {
            var result = await _accountService.GetAllAccountsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountResponse>> GetAccountById([BindRequired] int id)
        {
            var result = await _accountService.GetAccountByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CustomResponse>> CreateAccount(AccountRequest account)
        {
            var result = await _accountService.CreateAccountAsync(account);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomResponse>> DeleteAccount([BindRequired] int id)
        {
            var result = await _accountService.DeleteAccountAsync(id);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomResponse>> UpdateAccount([BindRequired] int id, AccountRequest account)
        {
            var result = await _accountService.UpdateAccountAsync(id, account);
            return Ok(result);
        }
    }
}
