using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Domain.Models.AccountDetails;

namespace CommonPortfolio.Api.Features.AccountDetails
{
    public class GetAccountDetailsEndpoint : EndpointWithoutRequest<List<AccountDetailsModel>>
    {
        private readonly IAccountDetailsService _acccountDetailsService;

        public GetAccountDetailsEndpoint(IAccountDetailsService acccountDetailsService)
        {
            _acccountDetailsService = acccountDetailsService;
        }

        public override void Configure()
        {
            Get("/api/account-details");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }
        public override async Task<List<AccountDetailsModel>> ExecuteAsync(CancellationToken ct)
        {
            return await _acccountDetailsService.GetAccountDetails(User.GetCurrentUserId());
        }
    }


}
