using CommonPortfolio.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CommonPortfolio.Domain.Interfaces.Context
{
    public interface IDBContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AccountDetails> AccountDetails { get; set; }
        public DbSet<AccountLinks> AccountLinks { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<ExperienceDetails> ExperienceDetails { get; set; }
        public DbSet<HighlightDetails> HighlightDetails { get; set; }
        public DbSet<SkillType> SkillTypes { get; set; }
        public DbSet<Skill> Skills { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DatabaseFacade GetDatabase();
    }
}
