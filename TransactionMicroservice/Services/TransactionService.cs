using System.Linq;
using TransactionMicroservice.DataAccess;
using TransactionMicroservice.Models;
using AccountMicroservice;

namespace TransactionMicroservice.Services
{
    public class TransactionService
    {
        private readonly TransactionDbContext _dbContext;
        private readonly AccountDbContext _accountDbContext;

        public TransactionService(TransactionDbContext dbContext, AccountDbContext accountDbContext)
        {
            _dbContext = dbContext;
            _accountDbContext = accountDbContext;
        }

        public decimal Deposit(int accountNumber, decimal amount)
        {
            var account = _accountDbContext.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (account == null)
                return -1; // Account not found

            account.Balance += amount;
            _dbContext.SaveChanges();

            var transaction = new Transaction
            {
                AccountNumber = accountNumber+"",
                Amount = amount,
                Type = TransactionType.Deposit
            };
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            return account.Balance;
        }



        public decimal Withdrawal(int accountNumber, decimal amount)
        {
            var account = _accountDbContext.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (account == null)
                return -1; // Account not found

            if (account.Balance >= amount)
            {
                account.Balance -= amount;
                _dbContext.SaveChanges();

                var transaction = new Transaction
                {
                    AccountNumber = accountNumber + "",
                    Amount = amount,
                    Type = TransactionType.Withdrawal
                };
                _dbContext.Transactions.Add(transaction);
                _dbContext.SaveChanges();

                return account.Balance;
            }
            else
            {
                return -2; // Insufficient funds
            }
        }

    }
}
