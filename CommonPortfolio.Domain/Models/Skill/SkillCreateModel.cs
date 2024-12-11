namespace CommonPortfolio.Domain.Models.Skill
{
    public class SkillCreateModel
    {
        public Guid UserId { get; set; }
        public required Guid SkillTypeId { get; set; }
        public required string Title { get; set; }
    }
}
