using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KameGameAPI.Models
{
    public class Set
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string setCode { get ; set ; }
        [Required]
        public string setName { get; set; }
        [Required]
        public string setRarity { get; set; }
    }
}
