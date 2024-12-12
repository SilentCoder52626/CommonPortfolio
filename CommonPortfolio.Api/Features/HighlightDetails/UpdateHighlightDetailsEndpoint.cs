using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Domain.Models.HighlightDetails;
using Microsoft.AspNetCore.Mvc;

namespace CommonPortfolio.Api.Features.HighlightDetails
{
    public class UpdateHighlightDetailsEndpoint : Endpoint<HighlightDetailsUpdateRequestModel, HighlightDetailsResponseModel>
    {
        private readonly IHighlightDetailsService _highlightDetailService;

        public UpdateHighlightDetailsEndpoint(IHighlightDetailsService highlightDetailService)
        {
            _highlightDetailService = highlightDetailService;
        }

        public override void Configure()
        {
            Put("/api/highlight/update/{Id}");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }
        public override async Task<HighlightDetailsResponseModel> ExecuteAsync(HighlightDetailsUpdateRequestModel req, CancellationToken ct)
        {
            await _highlightDetailService.Update(new HighlightDetailsUpdateModel() { Title = req.Title, Id = req.Id, Description = req.Description});
            return new HighlightDetailsResponseModel() { Message = "Highlight Details updated succesfully." };
        }
    }
    public class HighlightDetailsUpdateRequestModel
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        [FromRoute]
        public Guid Id { get; set; }

    }
    public class HighlightDetailsResponseModel
    {
        public required string Message { get; set; }
    }

}
