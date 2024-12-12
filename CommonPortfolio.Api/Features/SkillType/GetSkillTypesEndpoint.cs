using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Domain.Models.SkillType;

namespace CommonPortfolio.Api.Features.SkillType
{
    public class GetSkillTypesEndpoint : EndpointWithoutRequest<List<SkillTypeModel>>
    {
        private readonly ISkillTypeService _skillTypeService;

        public GetSkillTypesEndpoint(ISkillTypeService skillTypeService)
        {
            _skillTypeService = skillTypeService;
        }

        public override void Configure()
        {
            Get("/api/skill-type");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }
        public override Task<List<SkillTypeModel>> ExecuteAsync(CancellationToken ct)
        {
            return _skillTypeService.GetSkillTypes(User.GetCurrentUserId());
        }
    }


}
