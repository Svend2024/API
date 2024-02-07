using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KameGameAPI.Models
{
    public class Set
    {
        [Key]
        public int setId { get; set; }
        [Required]
        public string setCode { get ; set ; }
        [Required]
        public string setName { get; set; }
    }
}
