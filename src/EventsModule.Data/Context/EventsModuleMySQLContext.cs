using EventsModule.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsModule.Data.Context
{
    public class EventsModuleMySQLContext : DbContext
    {

        public EventsModuleMySQLContext()
        { }

        public EventsModuleMySQLContext(DbContextOptions<EventsModuleMySQLContext> options) : base(options)
        { }

        public virtual DbSet<User> Users { get; set; }
        
        public virtual DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Apply Entity Configurations
            builder.ApplyConfigurationsFromAssembly(typeof(EventsModuleMySQLContext).Assembly);
        }
    }
}
