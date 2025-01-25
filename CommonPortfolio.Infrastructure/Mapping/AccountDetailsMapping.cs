using CommonPortfolio.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommonPortfolio.Infrastructure.Mapping
{
    public class AccountDetailsMapping : IEntityTypeConfiguration<AccountDetails>
    {
        public void Configure(EntityTypeBuilder<AccountDetails> builder)
        {
            // Specify the table name
            builder.ToTable("AccountDetails");

            // Primary Key
            builder.HasKey(ad => ad.Id);

            // Configure properties
            builder.Property(ad => ad.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(ad => ad.UserId)
                .IsRequired();

            builder.Property(ad => ad.Position)
                .HasMaxLength(100); 

            builder.Property(ad => ad.SubName)
                .HasMaxLength(150); 
            
            builder.Property(ad => ad.CVLink)
                .HasMaxLength(250); 

            builder.Property(ad => ad.ProfilePictureLink)
                .HasMaxLength(1000); 

            builder.Property(ad => ad.BannerPictureLink)
                .HasMaxLength(1000);

            builder.Property(ad => ad.ProfilePicturePublicId)
               .HasMaxLength(1000);

            builder.Property(ad => ad.BannerPicturePublicId)
                .HasMaxLength(1000);

            builder.Property(ad => ad.ShortDescription)
                .HasMaxLength(1000); 

            builder.Property(ad => ad.DetailedDescription)
                .HasColumnType("text"); 

            // Define relationships
            builder.HasOne(ad => ad.User)
                .WithOne(u => u.AccountDetails)
                .HasForeignKey<AccountDetails>(ad => ad.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }

}
