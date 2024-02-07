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
        public string? type { get; set; }
        public string? attribute { get; set; }
        public string? race { get; set; }
        [Required]
        public string cardCode { get; set; }
        [ForeignKey("Set.setCode")]
        public string setCode { get; set; }
        public Set set { get; set; }
        [Required]
        public string pictureLink { get; set; }
        [Required]
        public int price { get; set; }
        [Required]
        public int stock { get; set; }
    }
}
