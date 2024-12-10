namespace CommonPortfolio.Domain.Entity
{
    public class SkillType
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

    }
}
