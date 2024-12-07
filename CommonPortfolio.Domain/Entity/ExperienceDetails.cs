namespace CommonPortfolio.Domain.Entity
{
    public class ExperienceDetails
    {
        public Guid Id { get; set; }
        public Guid ExperienceId { get; set; }
        public Experience Experience { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
    }
}
