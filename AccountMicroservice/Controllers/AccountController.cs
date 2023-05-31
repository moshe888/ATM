using AccountMicroservice.Models;
using AccountMicroservice.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{accountNumber}/balance")]
        public ActionResult<decimal> GetAccountBalance(int accountNumber)
        {
            var balance = _accountService.GetAccountBalance(accountNumber);
            return Ok(balance);
        }

        [HttpPost("{accountNumber}/balance")]
        public IActionResult UpdateAccountBalance(int accountNumber, decimal amount)
        {
            _accountService.UpdateAccountBalance(accountNumber, amount);
            return NoContent();
        }
    }
}
