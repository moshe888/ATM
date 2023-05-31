using TransactionMicroservice.Models;
using TransactionMicroservice.Services;
using Microsoft.AspNetCore.Mvc;

namespace TransactionMicroservice.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("deposit")]
        public ActionResult<decimal> Deposit(int accountNumber, decimal amount)
        {
            var newBalance = _transactionService.Deposit(accountNumber, amount);
            return Ok(newBalance);
        }

        [HttpPost("withdrawal")]
        public ActionResult<decimal> Withdrawal(int accountNumber, decimal amount)
        {
            var newBalance = _transactionService.Withdrawal(accountNumber, amount);
            return Ok(newBalance);
        }
    }
}
