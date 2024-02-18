using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KameGameAPI.Models
{
    public class TransactionHistory : BaseEntity
    {
        //[Key]
        //public int transactionHistoryId { get { return id; } set { id = value; } }
        [Required]
        public double totalPrice { get; set; }
        [ForeignKey("Customer.id")]
        public int customerId { get; set; }
        public Customer? customer { get; set; }
        [Required]
        public DateTime creationDate { get; set; }
    }
}
