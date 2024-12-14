namespace CommonPortfolio.Domain.Models.Experience
{
    public class ExperienceUpdateModel
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Organization { get; set; }
        public string? Duration { get; set; }
        public List<ExperienceDetailsModel> ExperienceDetails { get; set; } = [];
    }
}
