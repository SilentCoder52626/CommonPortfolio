namespace CommonPortfolio.Domain.Models.HighlightDetails
{
    public class HighlightDetailsUpdateModel
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
