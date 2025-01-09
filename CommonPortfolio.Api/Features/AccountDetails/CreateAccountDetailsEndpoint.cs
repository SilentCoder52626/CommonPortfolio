using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Domain.Models.AccountDetails;

namespace CommonPortfolio.Api.Features.AccountDetails
{
    public class CreateAccountDetailsEndpoint : Endpoint<AccountDetailsAddUpdateRequestModel, AccountDetailsModel>
    {
        private readonly IAccountDetailsService _accountDetailsService;
        public CreateAccountDetailsEndpoint(IAccountDetailsService accountDetailsService)
        {
            _accountDetailsService = accountDetailsService;
        }

        public override void Configure()
        {
            Post("/api/account-details/addorupdate");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
            AllowFormData();
        }

        public override async Task<AccountDetailsModel> ExecuteAsync(AccountDetailsAddUpdateRequestModel req, CancellationToken ct)
        {
            var createModel = new AccountDetailsAddUpdateModel
            {
                UserId = User.GetCurrentUserId(),
                Position = req.Position,
                SubName = req.SubName,
                ShortDescription = req.ShortDescription,
                DetailedDescription = req.DetailedDescription,
                ProfilePicture = req.ProfilePicture,
                BannerPicture = req.BannerPicture,
                DeleteBannerPicture = req.DeleteBannerPicture,
                DeleteProfilePicture = req.DeleteProfilePicture
            };

            return await _accountDetailsService.AddOrUpdate(createModel);
        }

    }
    public class AccountDetailsAddUpdateRequestModel
    {
        public string? Position { get; set; }
        public string? SubName { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public IFormFile? BannerPicture { get; set; }
        public string? ShortDescription { get; set; }
        public string? DetailedDescription { get; set; }
        public bool DeleteBannerPicture { get; set; }
        public bool DeleteProfilePicture { get; set; }
    }

}
