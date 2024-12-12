using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Domain.Models.Education;

namespace CommonPortfolio.Api.Features.Education
{
    public class GetEducationsEndpoint : EndpointWithoutRequest<List<EducationModel>>
    {
        private readonly IEducationService _educationService;

        public GetEducationsEndpoint(IEducationService educationService)
        {
            _educationService = educationService;
        }

        public override void Configure()
        {
            Get("/api/education");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }
        public override async Task<List<EducationModel>> ExecuteAsync(CancellationToken ct)
        {
            return await _educationService.GetEducations(User.GetCurrentUserId());
        }
    }


}
