namespace CommonPortfolio.Domain.Models.SkillType
{
    public class SkillTypeUpdateModel
    {
        public Guid Id { get; set; } 
        public required string Title { get; set; }
    }
}
