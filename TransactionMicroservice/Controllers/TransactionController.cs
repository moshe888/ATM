using TransactionMicroservice.Services;
using Microsoft.AspNetCore.Mvc;

namespace TransactionMicroservice.Controllers
{

    // This controller is responsible for handling transactions
    [Route("atm/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;
        
        // Inject TransactionService
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
            if (newBalance == -1)
            {
                return BadRequest("Insufficient funds");
            }
            if (newBalance == -2)
            {
                return BadRequest("Unable to dispense the requested amount with available bills");
            }

            return Ok(newBalance);
        }
    }
}
