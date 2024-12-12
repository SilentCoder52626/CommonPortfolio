using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Domain.Models.Education;
using Microsoft.AspNetCore.Mvc;

namespace CommonPortfolio.Api.Features.Education
{
    public class UpdateEducationEndpoint : Endpoint<EducationUpdateRequestModel, EducationResponseModel>
    {
        private readonly IEducationService _educationService;

        public UpdateEducationEndpoint(IEducationService educationService)
        {
            _educationService = educationService;
        }

        public override void Configure()
        {
            Put("/api/education/update/{Id}");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }
        public override async Task<EducationResponseModel> ExecuteAsync(EducationUpdateRequestModel req, CancellationToken ct)
        {
            await _educationService.Update(new EducationUpdateModel()
            {
                Title = req.Title,
                Id = req.Id,
                Address = req.Address,
                Description = req.Description,
                EndYear = req.EndYear,
                StartYear = req.StartYear,
                University = req.University

            });
            return new EducationResponseModel() { Message = "Education  updated succesfully." };
        }
    }
    public class EducationUpdateRequestModel
    {
        public required string Title { get; set; }
        [FromRoute]
        public Guid Id { get; set; }
        public required string University { get; set; }
        public required string Address { get; set; }
        public required string Description { get; set; }
        public required int StartYear { get; set; }
        public int? EndYear { get; set; }

    }
    public class EducationResponseModel
    {
        public required string Message { get; set; }
    }

}
