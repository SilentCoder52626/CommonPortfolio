using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Domain.Models.Experience;

namespace CommonPortfolio.Api.Features.Experience
{
    public class CreateExperienceEndpoint : Endpoint<ExperienceCreateRequestModel, ExperienceModel>
    {
        private readonly IExperienceService _experienceService;

        public CreateExperienceEndpoint(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        public override void Configure()
        {
            Post("/api/experience/create");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }

        public override async Task<ExperienceModel> ExecuteAsync(ExperienceCreateRequestModel req, CancellationToken ct)
        {
            return await _experienceService.Create(new ExperienceCreateModel()
            {
                Title = req.Title,
                UserId = User.GetCurrentUserId(),
                Duration = req.Duration,
                Organization = req.Organization,
                ExperienceDetails = req.ExperienceDetails
            });
        }

    }
    public class ExperienceCreateRequestModel
    {
        public required string Title { get; set; }
        public string? Organization { get; set; }
        public string? Duration { get; set; }
        public List<ExperienceDetailsModel> ExperienceDetails { get; set; } = [];


    }

}
