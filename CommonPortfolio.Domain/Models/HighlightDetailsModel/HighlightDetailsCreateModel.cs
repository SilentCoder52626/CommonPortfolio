namespace CommonPortfolio.Domain.Models.HighlightDetailsModel
{
    public class HighlightDetailsCreateModel
    {
        public required Guid UserId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
