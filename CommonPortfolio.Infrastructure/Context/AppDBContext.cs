﻿using CommonPortfolio.Domain.Entity;
using CommonPortfolio.Domain.Interfaces.Context;
using CommonPortfolio.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace CommonPortfolio.Infrastructure.Context
{
    public class AppDBContext : DbContext,IDBContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AccountDetails> AccountDetails { get; set; }
        public DbSet<AccountLinks> AccountLinks { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<HighlightDetails> HighlightDetails { get; set; }
        public DbSet<SkillType> SkillTypes { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Settings> Settings { get; set; }

        public DatabaseFacade GetDatabase()
        {
            return Database;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    }


}
