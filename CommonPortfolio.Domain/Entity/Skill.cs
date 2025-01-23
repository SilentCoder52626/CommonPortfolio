namespace CommonPortfolio.Domain.Entity
{
    public class Skill
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SkillTypeId { get; set; }
        public required string Title { get; set; }
        public string? IconClass { get; set; }
        public SkillType SkillType { get; set; }
        public AppUser User { get; set; }
    }
}
