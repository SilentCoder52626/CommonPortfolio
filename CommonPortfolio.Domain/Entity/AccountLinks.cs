namespace CommonPortfolio.Domain.Entity
{
    public class AccountLinks
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string Name { get; set; }
        public required string Url { get; set; }
        public AppUser User { get; set; }
    }
}
