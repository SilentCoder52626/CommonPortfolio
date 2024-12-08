using CommonPortfolio.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommonPortfolio.Infrastructure.Mapping
{
    public class AppUserMapping : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // Specify the table name
            builder.ToTable("AppUsers");

            // Primary Key
            builder.HasKey(u => u.Id);

            // Configure properties
            builder.Property(u => u.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.HasIndex(u => u.UserName)
            .IsUnique();

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(u => u.Email)
            .IsUnique();

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.Contact)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(u => u.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            // One-to-One with AccountDetails
            builder.HasOne(u => u.AccountDetails)
                .WithOne()
                .HasForeignKey<AccountDetails>(ad => ad.UserId) 
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many relationships
            builder.HasMany(u => u.Links)
                .WithOne()
                .HasForeignKey(al => al.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.HighlightDetails)
                .WithOne()
                .HasForeignKey(hd => hd.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Skills)
                .WithOne()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Experiences)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Educations)
                .WithOne()
                .HasForeignKey(ed => ed.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("IX_AppUser_Email");

            builder.HasIndex(u => u.UserName)
                .IsUnique()
                .HasDatabaseName("IX_AppUser_UserName");
        }
    }

}
