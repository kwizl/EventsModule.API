namespace EventsModule.Data.Models
{
    public class User : BaseEntity
    {
        public User()
        {
            Events = new HashSet<Event>();
        }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public virtual ICollection<Event>? Events { get; set; }
    }
}
