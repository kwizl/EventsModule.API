using EventsModule.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EventsModule.Data.Configuration
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(x => x.ID)
                .HasName("PK_Events_ID");

            builder.Property(x => x.Description)
                .HasMaxLength(300);

            builder.Property(x => x.DateCreated)
                .IsRequired();

            builder.Property(x => x.DateModified)
                .IsRequired();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.ID)
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

            //Setup complex indexes
            builder.HasIndex(x => x.ID)
                .HasDatabaseName("IX_Events_ID")
                .IsUnique();

            builder.HasIndex(x => x.Title)
                .HasDatabaseName("IX_Events_Title")
                .IsUnique();
        }
    }
}
