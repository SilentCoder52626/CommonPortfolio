using CommonPortfolio.Domain.Models.Experience;

namespace CommonPortfolio.Domain.Interfaces
{
    public interface IExperienceService
    {
        Task<List<ExperienceModel>> GetExperiences(Guid userId);
        Task<ExperienceModel> Create(ExperienceCreateModel model);
        Task Update(ExperienceUpdateModel model);
        Task Delete(Guid id);
    }
}
