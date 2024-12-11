namespace CommonPortfolio.Domain.Models.Skill
{
    public class SkillCreateModel
    {
        public Guid UserId { get; set; }
        public Guid SkillTypeId { get; set; }
        public required string Title { get; set; }
    }
}
