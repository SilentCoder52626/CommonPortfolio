namespace CommonPortfolio.Domain.Models.User
{
    public class UserModel : UserUpdateModel
    {
        public required string Role { get; set; }
        public required DateTime CreatedDate { get; set; }
    }
}
