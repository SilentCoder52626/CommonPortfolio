
using CommonPortfolio.Domain.Models.Skill;

namespace CommonPortfolio.Domain.Interfaces
{
    public interface ISkillService
    {
        Task<List<SkillModel>> GetSkills(Guid userId);
        Task<SkillModel> Create(SkillCreateModel model);
        Task Update(SkillUpdateModel model);
        Task Delete(Guid id);

    }
}
