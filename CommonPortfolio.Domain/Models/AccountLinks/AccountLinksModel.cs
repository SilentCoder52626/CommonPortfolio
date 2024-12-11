namespace CommonPortfolio.Domain.Models.AccountLinks
{
    public class AccountLinksModel
    {
        public required Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public required string Name { get; set; }
        public required string Url { get; set; }
    }

}
