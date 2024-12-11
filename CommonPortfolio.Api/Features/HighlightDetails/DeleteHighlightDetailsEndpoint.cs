using CommonBoilerPlateEight.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace CommonPortfolio.Api.Features.HighlightDetails
{
    public class DeleteHighlightDetailsEndpoint : Endpoint<DeleteHighlightDetailsRequest, DeleteHighlightDetailsResponse>
    {
        private readonly IHighlightDetailsService _highlightDetailsService;

        public DeleteHighlightDetailsEndpoint(IHighlightDetailsService hightlightDetailsService)
        {
            _highlightDetailsService = hightlightDetailsService;
        }

        public override void Configure()
        {
            Delete("/api/highlight/delete/{Id}");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }

        public override async Task<DeleteHighlightDetailsResponse> ExecuteAsync(DeleteHighlightDetailsRequest req, CancellationToken ct)
        {
            await _highlightDetailsService.Delete(req.Id);
            return new DeleteHighlightDetailsResponse() { Message = "Highlight Details removed successfully." };
        }
    }

    public class DeleteHighlightDetailsRequest
    {
        [FromRoute]
        public Guid Id { get; set; }
    }

    public class DeleteHighlightDetailsResponse
    {
        public string Message { get; set; }
    }

}
