using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Domain.Models.Skill;

namespace CommonPortfolio.Api.Features.Skill
{
    public class GetSkillsEndpoint : EndpointWithoutRequest<List<SkillModel>>
    {
        private readonly ISkillService _skillService;

        public GetSkillsEndpoint(ISkillService skillService)
        {
            _skillService = skillService;
        }

        public override void Configure()
        {
            Get("/api/skill");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }
        public override Task<List<SkillModel>> ExecuteAsync(CancellationToken ct)
        {
            return _skillService.GetSkills(User.GetCurrentUserId());
        }
    }


}
