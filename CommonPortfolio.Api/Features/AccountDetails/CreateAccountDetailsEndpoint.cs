using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Domain.Constants;
using CommonPortfolio.Domain.Helper.FileHelper;
using CommonPortfolio.Domain.Models.AccountDetails;

namespace CommonPortfolio.Api.Features.AccountDetails
{
    public class CreateAccountDetailsEndpoint : Endpoint<AccountDetailsCreateRequestModel, AccountDetailsModel>
    {
        private readonly IAccountDetailsService _accountDetailsService;
        private readonly IFileUploaderService _fileUploader;
        public CreateAccountDetailsEndpoint(IAccountDetailsService accountDetailsService, IFileUploaderService fileUploader)
        {
            _accountDetailsService = accountDetailsService;
            _fileUploader = fileUploader;
        }

        public override void Configure()
        {
            Post("/api/account-details/create");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
            AllowFormData();
        }

        public override async Task<AccountDetailsModel> ExecuteAsync(AccountDetailsCreateRequestModel req, CancellationToken ct)
        {
            var createModel = new AccountDetailsCreateModel
            {
                UserId = User.GetCurrentUserId(),
                Position = req.Position,
                SubName = req.SubName,
                ShortDescription = req.ShortDescription,
                DetailedDescription = req.DetailedDescription,
                ProfilePictureLink = req.ProfilePicture != null ? await _fileUploader.SaveFileAsync(req.ProfilePicture, FileDirectory.UserFileDirectory) : null,
                BannerPictureLink = req.BannerPicture != null ? await _fileUploader.SaveFileAsync(req.BannerPicture, FileDirectory.UserFileDirectory) : null
            };

            return await _accountDetailsService.Create(createModel);
        }

    }
    public class AccountDetailsCreateRequestModel
    {
        public string? Position { get; set; }
        public string? SubName { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public IFormFile? BannerPicture { get; set; }
        public string? ShortDescription { get; set; }
        public string? DetailedDescription { get; set; }

    }

}
