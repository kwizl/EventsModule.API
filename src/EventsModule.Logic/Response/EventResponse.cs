using System.ComponentModel.DataAnnotations;

namespace EventsModule.Logic.Response
{
    public sealed record EventResponse
    {
        public int ID { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int UserID { get; set; }

        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
