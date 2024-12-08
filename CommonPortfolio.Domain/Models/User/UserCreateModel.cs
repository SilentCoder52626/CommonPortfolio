namespace CommonPortfolio.Domain.Models.User
{
    public class UserCreateModel
    {
        public required string UserName { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Contact { get; set; }
        public required string Password { get; set; }
    }
}
