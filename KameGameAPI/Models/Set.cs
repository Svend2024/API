using System.ComponentModel.DataAnnotations;

namespace KameGameAPI.Models
{
    public class Set : BaseEntity
    {
        [Key]
        public string setCode { get ; set ; }
        [Required]
        public string setName { get; set; }
        [Required]
        public string setRarity { get; set; }
    }
}
