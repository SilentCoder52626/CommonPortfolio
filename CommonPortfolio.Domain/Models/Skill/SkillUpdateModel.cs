namespace CommonPortfolio.Domain.Models.Skill
{
    public class SkillUpdateModel
    {
        public Guid Id { get; set; }
        public Guid SkillTypeId { get; set; }
        public required string Title { get; set; }
    }
}
