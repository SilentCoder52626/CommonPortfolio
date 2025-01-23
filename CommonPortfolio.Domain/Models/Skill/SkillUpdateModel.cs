namespace CommonPortfolio.Domain.Models.Skill
{
    public class SkillUpdateModel
    {
        public Guid Id { get; set; }
        public required Guid SkillTypeId { get; set; }
        public required string Title { get; set; }
        public string? IconClass { get; set; }
    }
}
