using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Domain.Models.Experience;

namespace CommonPortfolio.Api.Features.Experience
{
    public class GetExperiencesEndpoint : EndpointWithoutRequest<List<ExperienceModel>>
    {
        private readonly IExperienceService _experienceService;

        public GetExperiencesEndpoint(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        public override void Configure()
        {
            Get("/api/experience");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }
        public override async Task<List<ExperienceModel>> ExecuteAsync(CancellationToken ct)
        {
            return await _experienceService.GetExperiences(User.GetCurrentUserId());
        }
    }


}
