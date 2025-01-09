using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Domain.Constants;
using CommonPortfolio.Domain.Entity;
using CommonPortfolio.Domain.Models.AccountDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommonPortfolio.Api.Features.AccountDetails
{
    public class UpdateAccountDetailsEndpoint : Endpoint<AccountDetailsUpdateRequestModel, AccountDetailsResponseModel>
    {
        private readonly IAccountDetailsService _accountDetailsService;

        public UpdateAccountDetailsEndpoint(IAccountDetailsService accountDetailsService)
        {
            _accountDetailsService = accountDetailsService;
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
            var updateModel = new AccountDetailsUpdateModel
            {
                Id = req.Id,
                Position = req.Position,
                SubName = req.SubName,
                ShortDescription = req.ShortDescription,
                DetailedDescription = req.DetailedDescription,
            };
            await _accountDetailsService.Update(updateModel);

            
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
