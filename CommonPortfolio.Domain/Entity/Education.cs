namespace CommonPortfolio.Domain.Entity
{
    public class Education
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string Title { get; set; }
        public required string University { get; set; }
        public required string Address { get; set; }
        public required string Description { get; set; }
        public required int StartYear { get; set; }
        public int? EndYear { get; set; }
    }
}
