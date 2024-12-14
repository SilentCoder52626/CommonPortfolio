using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Domain.Models.Experience;
using Microsoft.AspNetCore.Mvc;

namespace CommonPortfolio.Api.Features.Experience
{
    public class UpdateExperienceEndpoint : Endpoint<ExperienceUpdateRequestModel, ExperienceResponseModel>
    {
        private readonly IExperienceService _experienceService;

        public UpdateExperienceEndpoint(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        public override void Configure()
        {
            Put("/api/experience/update/{Id}");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }
        public override async Task<ExperienceResponseModel> ExecuteAsync(ExperienceUpdateRequestModel req, CancellationToken ct)
        {
            await _experienceService.Update(new ExperienceUpdateModel()
            {
                Title = req.Title,
                Id = req.Id,
                Duration = req.Duration,
                Organization = req.Organization,
                ExperienceDetails = req.ExperienceDetails

            });
            return new ExperienceResponseModel() { Message = "Experience  updated succesfully." };
        }
    }
    public class ExperienceUpdateRequestModel
    {
        public required string Title { get; set; }
        [FromRoute]
        public Guid Id { get; set; }
        public string? Organization { get; set; }
        public string? Duration { get; set; }
        public List<ExperienceDetailsModel> ExperienceDetails { get; set; } = [];

    }
    public class ExperienceResponseModel
    {
        public required string Message { get; set; }
    }

}
