using CommonPortfolio.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommonPortfolio.Infrastructure.Mapping
{
    public class SkillMapping : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            // Specify the table name
            builder.ToTable("Skills");

            // Primary Key
            builder.HasKey(s => s.Id);

            // Configure properties
            builder.Property(s => s.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(s => s.UserId)
                .IsRequired();

            builder.Property(s => s.SkillTypeId)
                .IsRequired();

            builder.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(s => s.IconClass)
               .HasMaxLength(150);

            // Define relationships
            builder.HasOne(s => s.User)
                .WithMany(u => u.Skills)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.SkillType)
            .WithMany()
            .HasForeignKey(s => s.SkillTypeId)
            .OnDelete(DeleteBehavior.Cascade);

            // Indexes (if needed)
            builder.HasIndex(s => s.UserId)
                .HasDatabaseName("IX_Skills_UserId");

            builder.HasIndex(s => s.SkillTypeId)
                .HasDatabaseName("IX_Skills_SkillTypeId");
        }
    }

}
