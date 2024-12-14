using CommonPortfolio.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommonPortfolio.Infrastructure.Mapping
{
    public class ExperienceMapping : IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            // Specify the table name
            builder.ToTable("Experiences");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Configure properties
            builder.Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.UserId)
                .IsRequired();

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Organization)
                .HasMaxLength(200);

            builder.Property(e => e.Duration)
                .HasMaxLength(200);

            // Define relationships
            builder.HasOne(e => e.User)
                .WithMany(u => u.Experiences)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.ExperienceDetails)
                .WithOne(ed => ed.Experience)
                .HasForeignKey(ed => ed.ExperienceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
