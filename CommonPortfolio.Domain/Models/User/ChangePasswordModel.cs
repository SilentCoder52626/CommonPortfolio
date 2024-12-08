namespace CommonPortfolio.Domain.Models.User
{
    public class ChangePasswordModel
    {
        public Guid Id { get; set; }
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
