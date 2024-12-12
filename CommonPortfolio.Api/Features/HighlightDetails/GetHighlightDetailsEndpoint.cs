using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Domain.Models.HighlightDetails;

namespace CommonPortfolio.Api.Features.HighlightDetails
{
    public class GetHighlightDetailsEndpoint : EndpointWithoutRequest<List<HighlightDetailsModel>>
    {
        private readonly IHighlightDetailsService _highlightDetailsService;

        public GetHighlightDetailsEndpoint(IHighlightDetailsService highlightDetailsService)
        {
            _highlightDetailsService = highlightDetailsService;
        }

        public override void Configure()
        {
            Get("/api/highlight");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }
        public override async Task<List<HighlightDetailsModel>> ExecuteAsync(CancellationToken ct)
        {
            return await _highlightDetailsService.GetHighlightDetails(User.GetCurrentUserId());
        }
    }


}
