namespace CommonPortfolio.Domain.Models.AccountLinks
{
    public class AccountLinksCreateModel
    {
        public required Guid UserId { get; set; }
        public required string Name { get; set; }
        public required string Url { get; set; }
    }

}
