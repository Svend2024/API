using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KameGameAPI.Models
{
    public class BaseEntity
    {
        //[NotMapped] // Betyder at attributten skal ekskluderes fra databasen
        [Key]
        public int id { get; set; }
    }
}
