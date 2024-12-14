namespace CommonPortfolio.Domain.Models.Experience
{
    public class ExperienceDetailsModel
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
    }
}
