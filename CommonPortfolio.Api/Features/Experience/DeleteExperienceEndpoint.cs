using CommonBoilerPlateEight.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace CommonPortfolio.Api.Features.Experience
{
    public class DeleteExperienceEndpoint : Endpoint<DeleteExperienceRequest, DeleteExperienceResponse>
    {
        private readonly IExperienceService _experienceService;

        public DeleteExperienceEndpoint(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        public override void Configure()
        {
            Delete("/api/experience/delete/{Id}");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }

        public override async Task<DeleteExperienceResponse> ExecuteAsync(DeleteExperienceRequest req, CancellationToken ct)
        {
            await _experienceService.Delete(req.Id);
            return new DeleteExperienceResponse() { Message = "Experience details removed successfully." };
        }
    }

    public class DeleteExperienceRequest
    {
        [FromRoute]
        public Guid Id { get; set; }
    }

    public class DeleteExperienceResponse
    {
        public string Message { get; set; }
    }

}
