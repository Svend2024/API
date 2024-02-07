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
        [ForeignKey("ZipCodeCity.zipCode")]
        public int zipCode { get; set; }
        public ZipCodeCity zipCodeCity { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string email { get; set; }
        [ForeignKey("Login.loginId")]
        public int loginId { get; set; }
        public Login login { get; set; }
    }
}
