using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KameGameAPI.Models
{
    public class ZipCodeCity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int zipCode { get { return id; } set { id = value; } }
        [Required]
        public string City { get; set; }
    }
}
