using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountMicroservice.Models
{
    [Table("Account", Schema = "atm")]

    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Account_Number")]
        public int AccountNumber { get; set; }
        [Column("Balance")]
        public decimal Balance { get; set; }
    }
}
