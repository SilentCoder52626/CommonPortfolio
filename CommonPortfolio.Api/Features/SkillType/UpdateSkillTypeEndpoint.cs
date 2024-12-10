using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Domain.Models.SkillType;
using Microsoft.AspNetCore.Mvc;

namespace CommonPortfolio.Api.Features.SkillType
{
    public class UpdateSkillTypeEndpoint : Endpoint<SkillTypeUpdateRequestModel, SkillTypeResponseModel>
    {
        private readonly ISkillTypeService _skillTypeService;

        public UpdateSkillTypeEndpoint(ISkillTypeService skillTypeService)
        {
            _skillTypeService = skillTypeService;
        }

        public override void Configure()
        {
            Put("/api/skill-type/update/{Id}");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }
        public override async Task<SkillTypeResponseModel> ExecuteAsync(SkillTypeUpdateRequestModel req, CancellationToken ct)
        {
            await _skillTypeService.Update(new SkillTypeUpdateModel() { Title = req.Title, Id = req.Id});
            return new SkillTypeResponseModel() { Message = "Skill Type updated succesfully." };
        }
    }
    public class SkillTypeUpdateRequestModel
    {
        public required string Title { get; set; }
        [FromRoute]
        public Guid Id { get; set; }

    }
    public class SkillTypeResponseModel
    {
        public required string Message { get; set; }
    }

}
