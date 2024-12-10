using CommonBoilerPlateEight.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace CommonPortfolio.Api.Features.SkillType
{
    public class DeleteSkillTypeEndpoint : Endpoint<DeleteSkillTypeRequest, DeleteSkillTypeResponse>
    {
        private readonly ISkillTypeService _skillTypeService;

        public DeleteSkillTypeEndpoint(ISkillTypeService skillTypeService)
        {
            _skillTypeService = skillTypeService;
        }

        public override void Configure()
        {
            Delete("/api/skill-type/delete/{Id}");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }

        public override async Task<DeleteSkillTypeResponse> ExecuteAsync(DeleteSkillTypeRequest req, CancellationToken ct)
        {
            await _skillTypeService.Delete(req.Id);
            return new DeleteSkillTypeResponse() { Message = "Skill type removed successfully." };
        }
    }

    public class DeleteSkillTypeRequest
    {
        [FromRoute]
        public Guid Id { get; set; }
    }

    public class DeleteSkillTypeResponse
    {
        public string Message { get; set; }
    }

}
