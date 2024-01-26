using System.ComponentModel.DataAnnotations;

namespace KameGameAPI.Models
{
    public class ZipCodeCity : BaseEntity
    {
        [Key]
        public int zipCode { get { return id; } set { id = value; } }
        [Required]
        public string City { get; set; }
    }
}
