namespace CommonPortfolio.Domain.Entity
{
    public class HighlightDetails
    {

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

    }
}
