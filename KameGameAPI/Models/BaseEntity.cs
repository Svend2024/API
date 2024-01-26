using System.ComponentModel.DataAnnotations.Schema;

namespace KameGameAPI.Models
{
    public class BaseEntity
    {
        [NotMapped] // Betyder at attributten skal ekskluderes fra databasen
        public int id { get; set; }
    }
}
