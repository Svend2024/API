using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KameGameAPI.Models
{
    public class ProductManager : BaseEntity
    {
        [Key]
        public int productManagerId { get { return id; } set { id = value; } }
        [Required]
        public string fullname { get; set; }
        [ForeignKey("Login.loginId")]
        public int loginId { get; set; }
        public Login login { get; set; }

    }
}
