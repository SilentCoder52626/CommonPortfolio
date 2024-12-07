using CommonPortfolio.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommonPortfolio.Infrastructure.Mapping
{
    public class HighlightDetailsMapping : IEntityTypeConfiguration<HighlightDetails>
    {
        public void Configure(EntityTypeBuilder<HighlightDetails> builder)
        {
            // Specify the table name
            builder.ToTable("HighlightDetails");

            // Primary Key
            builder.HasKey(hd => hd.Id);

            // Configure properties
            builder.Property(hd => hd.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(hd => hd.UserId)
                .IsRequired();

            builder.Property(hd => hd.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(hd => hd.Description)
                .IsRequired()
                .HasColumnType("text"); 

            // Define relationships
            builder.HasOne(hd => hd.User)
                .WithMany(u => u.HighlightDetails)
                .HasForeignKey(hd => hd.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(hd => hd.UserId)
                .HasDatabaseName("IX_HighlightDetails_UserId");
        }
    }

}
