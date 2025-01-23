namespace CommonPortfolio.Domain.Entity
{
    public class AppUser
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Contact { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
        public required DateTime CreatedDate { get; set; }
        public required bool IsDeleted { get; set; }

        public AccountDetails AccountDetails { get; set; }
        public List<AccountLinks> Links { get; set; }
        public List<HighlightDetails> HighlightDetails { get; set; }
        public List<Skill> Skills { get; set; }
        public List<SkillType> SkillTypes { get; set; }
        public List<Experience> Experiences { get; set; }
        public List<Education> Educations { get; set; }
        public Settings Settings { get; set; }

    }
}
