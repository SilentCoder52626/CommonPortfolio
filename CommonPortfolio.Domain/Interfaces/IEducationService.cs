using CommonPortfolio.Domain.Models.Education;

namespace CommonPortfolio.Domain.Interfaces
{
    public interface IEducationService
    {
        Task<List<EducationModel>> GetEducations(Guid userId);
        Task<EducationModel> Create(EducationCreateModel model);
        Task Update(EducationUpdateModel model);
        Task Delete(Guid id);
    }
}
