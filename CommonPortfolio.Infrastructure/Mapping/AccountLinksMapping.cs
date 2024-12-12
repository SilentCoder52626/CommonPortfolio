using CommonPortfolio.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommonPortfolio.Infrastructure.Mapping
{
    public class AccountLinksMapping : IEntityTypeConfiguration<AccountLinks>
    {
        public void Configure(EntityTypeBuilder<AccountLinks> builder)
        {
            // Specify the table name
            builder.ToTable("AccountLinks");

            // Primary Key
            builder.HasKey(al => al.Id);

            // Configure properties
            builder.Property(al => al.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.UserId)
                .IsRequired();

            builder.Property(al => al.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(al => al.Url)
                .IsRequired()
                .HasMaxLength(500);

            // Define relationships
            builder.HasOne(al => al.User)
                .WithMany(u => u.Links)
                .HasForeignKey(al => al.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }

}
