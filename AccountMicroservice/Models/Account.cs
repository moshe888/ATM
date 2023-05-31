using System.ComponentModel.DataAnnotations;

namespace AccountMicroservice.Models
{
    public class Account
    {
        [Key]
        public int AccountNumber { get; set; }

        public decimal Balance { get; set; }
    }
}
