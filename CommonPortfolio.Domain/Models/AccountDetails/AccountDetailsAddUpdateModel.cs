
using Microsoft.AspNetCore.Http;

namespace CommonPortfolio.Domain.Models.AccountDetails
{
    public class AccountDetailsAddUpdateModel
    {
        public Guid UserId { get; set; }
        public string? Position { get; set; }
        public string? SubName { get; set; }
        public string? CVLink { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public IFormFile? BannerPicture { get; set; }
        public string? ShortDescription { get; set; }
        public string? DetailedDescription { get; set; }

        public bool DeleteBannerPicture { get; set; }
        public bool DeleteProfilePicture { get; set; }
    }
}
