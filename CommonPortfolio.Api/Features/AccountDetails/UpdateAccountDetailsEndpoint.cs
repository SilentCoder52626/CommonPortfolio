using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Domain.Constants;
using CommonPortfolio.Domain.Entity;
using CommonPortfolio.Domain.Helper.FileHelper;
using CommonPortfolio.Domain.Models.AccountDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommonPortfolio.Api.Features.AccountDetails
{
    public class UpdateAccountDetailsEndpoint : Endpoint<AccountDetailsUpdateRequestModel, AccountDetailsResponseModel>
    {
        private readonly IAccountDetailsService _accountDetailsService;
        private readonly IFileUploaderService _fileUploader;

        public UpdateAccountDetailsEndpoint(IAccountDetailsService accountDetailsService, IFileUploaderService fileUploader)
        {
            _accountDetailsService = accountDetailsService;
            _fileUploader = fileUploader;
        }

        public override void Configure()
        {
            Put("/api/account-details/update/{Id}");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
            AllowFormData();
        }
        public override async Task<AccountDetailsResponseModel> ExecuteAsync(AccountDetailsUpdateRequestModel req, CancellationToken ct)
        {
            var  accountDetails = await _accountDetailsService.GetById(req.Id);
            var existingProfileLink = accountDetails.ProfilePictureLink;
            var existingBannerLink = accountDetails.BannerPictureLink;
            var updateModel = new AccountDetailsUpdateModel
            {
                Id = req.Id,
                Position = req.Position,
                SubName = req.SubName,
                ShortDescription = req.ShortDescription,
                DetailedDescription = req.DetailedDescription,
                ProfilePictureLink = req.ProfilePicture != null ? await _fileUploader.SaveFileAsync(req.ProfilePicture, FileDirectory.UserFileDirectory) : null,
                BannerPictureLink = req.BannerPicture != null ? await _fileUploader.SaveFileAsync(req.BannerPicture, FileDirectory.UserFileDirectory) : null
            };
            await _accountDetailsService.Update(updateModel);

            if(req.ProfilePicture != null)
            {
                if(!String.IsNullOrEmpty(existingProfileLink))
                {
                    _fileUploader.RemoveFile(existingProfileLink);
                }
            }
            if(req.BannerPicture != null)
            {
                if(!String.IsNullOrEmpty(existingBannerLink))
                {
                    _fileUploader.RemoveFile(existingBannerLink);
                }
            }
            return new AccountDetailsResponseModel() { Message = "Account details updated succesfully." };
        }
    }
    public class AccountDetailsUpdateRequestModel
    {
        public string? Position { get; set; }
        public string? SubName { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public IFormFile? BannerPicture { get; set; }
        public string? ShortDescription { get; set; }
        public string? DetailedDescription { get; set; }

        [FromRoute]
        public Guid Id { get; set; }

    }
    public class AccountDetailsResponseModel
    {
        public required string Message { get; set; }
    }

}
