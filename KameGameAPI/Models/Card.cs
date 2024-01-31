using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KameGameAPI.Models
{
    public class Card : BaseEntity
    {
        [Key]
        public int cardId { get { return id; } set { id = value; } }
        [Required]
        public string name { get; set; }
        [Required]
        public string type { get; set; }
        [ForeignKey("Set.setKode")]
        public int setCode { get; set; }
        public Set set { get; set; }
        [Required]
        public string pictureLink { get; set; }
        public int stock { get; set; }
    }
}
