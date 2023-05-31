namespace TransactionMicroservice.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
    }

    public enum TransactionType
    {
        Deposit,
        Withdrawal
    }
}
