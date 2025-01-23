using CommonPortfolio.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommonPortfolio.Infrastructure.Mapping
{
    public class SettingsMapping : IEntityTypeConfiguration<Settings>
    {
        public void Configure(EntityTypeBuilder<Settings> builder)
        {
            builder.ToTable("Settings");

            builder.HasKey(ad => ad.Id);

            builder.Property(ad => ad.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(ad => ad.UserId)
                    .IsRequired();

            builder.Property(ad => ad.WEB3FormsAcessKey)
                    .HasMaxLength(200);

            builder.Property(ad => ad.Theme)
                .IsRequired().HasMaxLength(200);

            builder.HasOne(e => e.User)
                .WithOne(u => u.Settings)
                .HasForeignKey<Settings>(ad => ad.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
