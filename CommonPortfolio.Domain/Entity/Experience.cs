namespace CommonPortfolio.Domain.Entity
{
    public class Experience
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string Title { get; set; }
        public required string Organization { get; set; }
        public List<ExperienceDetails> ExperienceDetails { get; set; }

    }
}
