using System.ComponentModel.DataAnnotations;

namespace EventsModule.Data.Models
{
    public class BaseEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime DateModified { get; set; }
    }
}
