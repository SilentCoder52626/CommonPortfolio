namespace CommonPortfolio.Domain.Models.Experience
{
    public class ExperienceCreateModel
    {
        public Guid UserId { get; set; }
        public required string Title { get; set; }
        public string? Organization { get; set; }
        public string? Duration { get; set; }
        public List<ExperienceDetailsModel> ExperienceDetails { get; set; } = [];
    }
}
