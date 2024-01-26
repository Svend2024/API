using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KameGameAPI.Models
{
    public class Customer : BaseEntity
    {
        [Key]
        public int customerId { get { return id; } set { id = value; } }
        [Required]
        public string fullname { get; set; }
        [ForeignKey("PostNrBy.Postnummer")]
        public int zipCode { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string email { get; set; }
        [ForeignKey("Login.loginId")]
        public int loginId { get; set; }

    }
}
