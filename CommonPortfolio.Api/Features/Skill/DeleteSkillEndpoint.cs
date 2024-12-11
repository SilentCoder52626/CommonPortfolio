using CommonBoilerPlateEight.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace CommonPortfolio.Api.Features.Skill
{
    public class DeleteSkillEndpoint : Endpoint<DeleteSkillRequest, DeleteSkillResponse>
    {
        private readonly ISkillService _skillService;

        public DeleteSkillEndpoint(ISkillService skillService)
        {
            _skillService = skillService;
        }

        public override void Configure()
        {
            Delete("/api/skill/delete/{Id}");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }

        public override async Task<DeleteSkillResponse> ExecuteAsync(DeleteSkillRequest req, CancellationToken ct)
        {
            await _skillService.Delete(req.Id);
            return new DeleteSkillResponse() { Message = "Skill removed successfully." };
        }
    }

    public class DeleteSkillRequest
    {
        [FromRoute]
        public Guid Id { get; set; }
    }

    public class DeleteSkillResponse
    {
        public string Message { get; set; }
    }

}
