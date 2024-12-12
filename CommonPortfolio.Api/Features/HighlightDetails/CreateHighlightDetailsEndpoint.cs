using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Domain.Models.HighlightDetails;

namespace CommonPortfolio.Api.Features.HighlightDetails
{
    public class CreateHighlightDetailsEndpoint : Endpoint<HighlightDetailsCreateRequestModel, HighlightDetailsModel>
    {
        private readonly IHighlightDetailsService _highlightService;

        public CreateHighlightDetailsEndpoint(IHighlightDetailsService highlightService)
        {
            _highlightService = highlightService;
        }

        public override void Configure()
        {
            Post("/api/highlight/create");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }

        public override async Task<HighlightDetailsModel> ExecuteAsync(HighlightDetailsCreateRequestModel req, CancellationToken ct)
        {
            return await _highlightService.Create(new HighlightDetailsCreateModel() { Title = req.Title, UserId = User.GetCurrentUserId(), Description = req.Description });
        }
        
    }
    public class HighlightDetailsCreateRequestModel
    {
        public required string Title { get; set; }
        public required string Description { get; set; }

    }

}
