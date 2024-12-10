using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Domain.Models.SkillType;

namespace CommonPortfolio.Api.Features.SkillType
{
    public class CreateSkillTypeEndpoint : Endpoint<SkillTypeCreateRequestModel, SkillTypeModel>
    {
        private readonly ISkillTypeService _skillTypeService;

        public CreateSkillTypeEndpoint(ISkillTypeService skillTypeService)
        {
            _skillTypeService = skillTypeService;
        }

        public override void Configure()
        {
            Post("/api/skill-type/create");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }

        public override async Task<SkillTypeModel> ExecuteAsync(SkillTypeCreateRequestModel req, CancellationToken ct)
        {
            return await _skillTypeService.Create(new SkillTypeCreateModel() { Title = req.Title, UserId = User.ToTokenUser().Id});
        }
        
    }
    public class SkillTypeCreateRequestModel
    {
        public required string Title { get; set; }

    }

}
