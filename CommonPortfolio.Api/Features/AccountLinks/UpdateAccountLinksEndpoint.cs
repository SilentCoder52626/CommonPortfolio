using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Domain.Models.AccountLinks;
using Microsoft.AspNetCore.Mvc;

namespace CommonPortfolio.Api.Features.AccountLinks
{
    public class UpdateAccountLinksEndpoint : Endpoint<AccountLinksUpdateRequestModel, AccountLinksResponseModel>
    {
        private readonly IAccountLinksService _accountLinksService;

        public UpdateAccountLinksEndpoint(IAccountLinksService accountLinksService)
        {
            _accountLinksService = accountLinksService;
        }

        public override void Configure()
        {
            Put("/api/account-link/update/{Id}");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }
        public override async Task<AccountLinksResponseModel> ExecuteAsync(AccountLinksUpdateRequestModel req, CancellationToken ct)
        {
            await _accountLinksService.Update(new AccountLinksUpdateModel() { Name = req.Name, Id = req.Id, Url = req.Url});
            return new AccountLinksResponseModel() { Message = "Account Link updated succesfully." };
        }
    }
    public class AccountLinksUpdateRequestModel
    {
        public required string Name { get; set; }
        public required string Url { get; set; }
        [FromRoute]
        public Guid Id { get; set; }

    }
    public class AccountLinksResponseModel
    {
        public required string Message { get; set; }
    }

}
