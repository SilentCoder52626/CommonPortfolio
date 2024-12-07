namespace CommonPortfolio.Domain.Entity
{
    public class AppUser
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Contact { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
    }
}
