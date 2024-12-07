using CommonPortfolio.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommonPortfolio.Infrastructure.Mapping
{
    public class ExperienceDetailsMapping : IEntityTypeConfiguration<ExperienceDetails>
    {
        public void Configure(EntityTypeBuilder<ExperienceDetails> builder)
        {
            // Specify the table name
            builder.ToTable("ExperienceDetails");

            // Primary Key
            builder.HasKey(ed => ed.Id);

            // Configure properties
            builder.Property(ed => ed.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(ed => ed.ExperienceId)
                .IsRequired();

            builder.Property(ed => ed.Title)
                .IsRequired()
                .HasMaxLength(150); 

            builder.Property(ed => ed.Description)
                .IsRequired()
                .HasColumnType("text");

            // Define relationships
            builder.HasOne(ed => ed.Experience)
                .WithMany(e => e.ExperienceDetails)
                .HasForeignKey(ed => ed.ExperienceId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }

}
