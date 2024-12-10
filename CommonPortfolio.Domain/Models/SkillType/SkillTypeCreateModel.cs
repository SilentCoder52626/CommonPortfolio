namespace CommonPortfolio.Domain.Models.SkillType
{
    public class SkillTypeCreateModel
    {
        public required string Title { get; set; }
        public Guid UserId { get; set; }

    }
}
