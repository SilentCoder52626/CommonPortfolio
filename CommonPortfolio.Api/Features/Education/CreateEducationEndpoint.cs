using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Domain.Models.Education;

namespace CommonPortfolio.Api.Features.Education
{
    public class CreateEducationEndpoint : Endpoint<EducationCreateRequestModel, EducationModel>
    {
        private readonly IEducationService _educationService;

        public CreateEducationEndpoint(IEducationService educationService)
        {
            _educationService = educationService;
        }

        public override void Configure()
        {
            Post("/api/education/create");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }

        public override async Task<EducationModel> ExecuteAsync(EducationCreateRequestModel req, CancellationToken ct)
        {
            return await _educationService.Create(new EducationCreateModel()
            {
                Title = req.Title,
                UserId = User.GetCurrentUserId(),
                Address = req.Address,
                Description = req.Description,
                EndYear = req.EndYear,
                StartYear = req.StartYear,
                University= req.University
            });
        }

    }
    public class EducationCreateRequestModel
    {
        public required string Title { get; set; }
        public required string University { get; set; }
        public required string Address { get; set; }
        public required string Description { get; set; }
        public required int StartYear { get; set; }
        public int? EndYear { get; set; }

    }

}
