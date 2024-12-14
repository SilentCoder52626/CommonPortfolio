namespace CommonPortfolio.Domain.Entity
{
    public class Experience
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        public required string Title { get; set; }
        public string? Organization { get; set; }
        public string? Duration { get; set; }
        public List<ExperienceDetails> ExperienceDetails { get; set; }

    }
}
