using System.Linq;
using AccountMicroservice.Models;
using System.Linq;

namespace AccountMicroservice.Services
{
    public class AccountService
    {
        private readonly AccountDbContext _dbContext;

        public AccountService(AccountDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public decimal GetAccountBalance(int accountNumber)
        {
            var account = _dbContext.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            return account?.Balance ?? 0;
        }

        public void UpdateAccountBalance(int accountNumber, decimal amount)
        {
            var account = _dbContext.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (account != null)
            {
                account.Balance += amount;
                _dbContext.SaveChanges();
            }
        }

        public Account CreateAccount()
        {
            var account = new Account();
            account.Balance = 0;
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
            return account;
        }
    }
}
