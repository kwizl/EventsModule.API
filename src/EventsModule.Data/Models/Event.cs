using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventsModule.Data.Models
{
    public class Event : BaseEntity
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        
        public int UserID { get; set; }
    }
}
