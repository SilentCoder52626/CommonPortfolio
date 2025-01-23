using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Domain.Models.Skill;
using Microsoft.AspNetCore.Mvc;

namespace CommonPortfolio.Api.Features.Skill
{
    public class UpdateSkillEndpoint : Endpoint<SkillUpdateRequestModel, SkillResponseModel>
    {
        private readonly ISkillService _skillService;

        public UpdateSkillEndpoint(ISkillService skillService)
        {
            _skillService = skillService;
        }

        public override void Configure()
        {
            Put("/api/skill/update/{Id}");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }
        public override async Task<SkillResponseModel> ExecuteAsync(SkillUpdateRequestModel req, CancellationToken ct)
        {
            await _skillService.Update(new SkillUpdateModel() { Title = req.Title, Id = req.Id, SkillTypeId = req.SkillTypeId, IconClass = req.IconClass });
            return new SkillResponseModel() { Message = "Skill updated succesfully." };
        }
    }
    public class SkillUpdateRequestModel
    {
        public required string Title { get; set; }
        public string? IconClass { get; set; }
        public required Guid SkillTypeId { get; set; }
        [FromRoute]
        public Guid Id { get; set; }

    }
    public class SkillResponseModel
    {
        public required string Message { get; set; }
    }

}
