using CommonBoilerPlateEight.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace CommonPortfolio.Api.Features.Education
{
    public class DeleteEducationEndpoint : Endpoint<DeleteEducationRequest, DeleteEducationResponse>
    {
        private readonly IEducationService _educationService;

        public DeleteEducationEndpoint(IEducationService educationService)
        {
            _educationService = educationService;
        }

        public override void Configure()
        {
            Delete("/api/education/delete/{Id}");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }

        public override async Task<DeleteEducationResponse> ExecuteAsync(DeleteEducationRequest req, CancellationToken ct)
        {
            await _educationService.Delete(req.Id);
            return new DeleteEducationResponse() { Message = "Education details removed successfully." };
        }
    }

    public class DeleteEducationRequest
    {
        [FromRoute]
        public Guid Id { get; set; }
    }

    public class DeleteEducationResponse
    {
        public string Message { get; set; }
    }

}
