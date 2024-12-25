using CommonPortfolio.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommonPortfolio.Infrastructure.Mapping
{
    public class SkillTypeMapping : IEntityTypeConfiguration<SkillType>
    {
        public void Configure(EntityTypeBuilder<SkillType> builder)
        {
            builder.ToTable("SkillType");

            // Primary Key
            builder.HasKey(u => u.Id);

            // Configure properties
            builder.Property(u => u.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Title)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasOne(s => s.User)
               .WithMany(u => u.SkillTypes)
               .HasForeignKey(s => s.UserId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }

}
