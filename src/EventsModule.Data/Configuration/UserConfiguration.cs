using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using EventsModule.Data.Models;

namespace EventsModule.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.ID)
                .HasName("PK_Users_ID");

            builder.Property(x => x.Description)
                .HasMaxLength(300);

            builder.Property(x => x.DateCreated)
                .IsRequired();

            builder.Property(x => x.DateModified)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(250);

            // Foreign Keys
            builder.HasMany(x => x.Events)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            //Setup complex indexes
            builder.HasIndex(x => x.ID)
                .HasDatabaseName("IX_Users_ID")
                .IsUnique();

            builder.HasIndex(x => x.Name)
                .HasDatabaseName("IX_Users_Name")
                .IsUnique();

            builder.Property(x => x.ID)
                .ValueGeneratedNever();

            // Seeds User data
            builder.HasData
            (
                new User
                {
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Description = "Testing API",
                    Name = "Tester",
                    ID = 1
                }
            );
        }
    }
}
