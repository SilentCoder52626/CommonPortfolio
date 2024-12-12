using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Domain.Models.Skill;

namespace CommonPortfolio.Api.Features.Skill
{
    public class CreateSkillEndpoint : Endpoint<SkillCreateRequestModel, SkillModel>
    {
        private readonly ISkillService _skillService;

        public CreateSkillEndpoint(ISkillService skillService)
        {
            _skillService = skillService;
        }

        public override void Configure()
        {
            Post("/api/skill/create");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }

        public override async Task<SkillModel> ExecuteAsync(SkillCreateRequestModel req, CancellationToken ct)
        {
            return await _skillService.Create(new SkillCreateModel() { Title = req.Title, UserId = User.GetCurrentUserId(), SkillTypeId = req.SkillTypeId });
        }
        
    }
    public class SkillCreateRequestModel
    {
        public required string Title { get; set; }
        public required Guid SkillTypeId { get; set; }

    }

}
