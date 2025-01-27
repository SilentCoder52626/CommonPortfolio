﻿using CommonPortfolio.Domain.Entity;
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
                .WithOne(a=>a.User)
                .HasForeignKey<AccountDetails>(ad => ad.UserId) 
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(u => u.Settings)
                .WithOne(a=>a.User)
                .HasForeignKey<Settings>(ad => ad.UserId) 
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many relationships
            builder.HasMany(u => u.Links)
                .WithOne(a => a.User)
                .HasForeignKey(al => al.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.HighlightDetails)
                .WithOne(a => a.User)
                .HasForeignKey(hd => hd.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.SkillTypes)
                .WithOne(a => a.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

             builder.HasMany(u => u.Skills)
                .WithOne(a => a.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Experiences)
                .WithOne(a => a.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Educations)
                .WithOne(a => a.User)
                .HasForeignKey(ed => ed.UserId)
                .OnDelete(DeleteBehavior.NoAction);

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
