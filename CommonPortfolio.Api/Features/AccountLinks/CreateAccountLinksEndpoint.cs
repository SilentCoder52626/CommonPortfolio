using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Domain.Models.AccountLinks;

namespace CommonPortfolio.Api.Features.AccountLinks
{
    public class CreateAccountLinksEndpoint : Endpoint<AccountLinksCreateRequestModel, AccountLinksModel>
    {
        private readonly IAccountLinksService _accountLinksService;

        public CreateAccountLinksEndpoint(IAccountLinksService accountLinkService)
        {
            _accountLinksService = accountLinkService;
        }

        public override void Configure()
        {
            Post("/api/account-link/create");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }

        public override async Task<AccountLinksModel> ExecuteAsync(AccountLinksCreateRequestModel req, CancellationToken ct)
        {
            return await _accountLinksService.Create(new AccountLinksCreateModel() { Name = req.Title, UserId = User.ToTokenUser().Id, Url = req.Url });
        }
        
    }
    public class AccountLinksCreateRequestModel
    {
        public required string Title { get; set; }
        public required string Url { get; set; }

    }

}
