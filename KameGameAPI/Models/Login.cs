using System.ComponentModel.DataAnnotations;

namespace KameGameAPI.Models
{
    public class Login : BaseEntity
    {
        //[Key]
        //public int loginId { get { return id; } set { id = value; } }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
