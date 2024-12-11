namespace CommonPortfolio.Domain.Models.AccountLinks
{
    public class AccountLinksUpdateModel
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Url { get; set; }
    }

}
