using System;
using System.Collections.Generic;
using System.Linq;
using AccountMicroservice.Services;

namespace TransactionMicroservice.Services
{
    public class TransactionService
    {
        private readonly AccountService accountService;
        private readonly Dictionary<int, int> billInventory;

        public TransactionService(AccountService service)
        {
            accountService = service;

        }


        // Update the account balance by depositing the amount
        public decimal Deposit(int accountNumber, decimal amount)
        {
            accountService.UpdateAccountBalance(accountNumber, amount);

             return accountService.GetAccountBalance(accountNumber);
        }

        public decimal Withdrawal(int accountNumber, decimal amount)
        {
            if (accountService.GetAccountBalance(accountNumber) < amount)
            {
                return -1; // Insufficient funds
            }

            // Get the bill counts required for the withdrawal amount
            var billCounts = GetBillCounts(amount);

            if (billCounts == null)
            {
                return -2; // Unable to dispense the requested amount with available bills
            }

            // update the bill inventory 
            DeductBills(billCounts);

            // Update the account balance 
            accountService.UpdateAccountBalance(accountNumber, -amount);

            return accountService.GetAccountBalance(accountNumber);
        }

        private Dictionary<int, int> GetBillCounts(decimal amount)
        {
            Dictionary<int, int> billCounts = new Dictionary<int, int>();

            // Iterate through the bill inventory in descending order of denominations
            foreach (var kvp in BillInventory.Stock.OrderByDescending(x => x.Key))
            {
                var billDenomination = kvp.Key;
                var billCount = kvp.Value;

                // Calculate the number of bills to dispense for the current denomination
                var billsToDispense = (int)(amount / billDenomination);

                // Calculate the actual number of bills to deduct (limited by inventory count)
                var billsToDeduct = Math.Min(billsToDispense, billCount);

                if (billsToDeduct > 0)
                {
                    // Store the count of dispensed bills for the current denomination
                    billCounts[billDenomination] = billsToDeduct;

                    // Update the remaining amount after deducting the current denomination bills
                    amount -= billDenomination * billsToDeduct;
                }
            }

            // Check if the entire amount has been dispensed in bills
            return amount == 0 ? billCounts : null;
        }

        private void DeductBills(Dictionary<int, int> billCounts)
        {
            // Deduct the dispensed bills from the bill inventory
            foreach (var kvp in billCounts)
            {
                BillInventory.Stock[kvp.Key] -= kvp.Value;

            }
        }
    }
}
