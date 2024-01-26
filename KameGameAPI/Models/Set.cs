using System.ComponentModel.DataAnnotations;

namespace KameGameAPI.Models
{
    public class Set : BaseEntity
    {
        [Key]
        public int setCode { get { return id; } set { id = value; } }
        [Required]
        public string setName { get; set; }
        [Required]
        public string setRarity { get; set; }
    }
}
