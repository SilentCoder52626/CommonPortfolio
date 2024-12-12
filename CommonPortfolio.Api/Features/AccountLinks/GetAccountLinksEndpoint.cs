using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Domain.Models.AccountLinks;

namespace CommonPortfolio.Api.Features.AccountLinks
{
    public class GetAccountLinksEndpoint : EndpointWithoutRequest<List<AccountLinksModel>>
    {
        private readonly IAccountLinksService _accountLinksService;

        public GetAccountLinksEndpoint(IAccountLinksService accountLinksService)
        {
            _accountLinksService = accountLinksService;
        }

        public override void Configure()
        {
            Get("/api/account-link");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }
        public override Task<List<AccountLinksModel>> ExecuteAsync(CancellationToken ct)
        {
            return _accountLinksService.GetAccountLinks(User.GetCurrentUserId());
        }
    }


}
