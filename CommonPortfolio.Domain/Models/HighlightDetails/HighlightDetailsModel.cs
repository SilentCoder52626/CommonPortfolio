namespace CommonPortfolio.Domain.Models.HighlightDetails
{
    public class HighlightDetailsModel
    {
        public required Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
