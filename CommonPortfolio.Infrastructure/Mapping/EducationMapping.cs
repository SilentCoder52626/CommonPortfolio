using CommonPortfolio.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommonPortfolio.Infrastructure.Mapping
{
    public class EducationMapping : IEntityTypeConfiguration<Education>
    {
        public void Configure(EntityTypeBuilder<Education> builder)
        {
            // Specify the table name
            builder.ToTable("Educations");

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

            builder.Property(e => e.University)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(e => e.StartYear)
                .IsRequired();

            builder.Property(e => e.EndYear);

            // Define relationships
            builder.HasOne(e => e.User)
                .WithMany(u => u.Educations)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }

}
