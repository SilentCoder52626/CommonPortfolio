using CommonPortfolio.Domain.Models.SkillType;

namespace CommonPortfolio.Domain.Interfaces
{
    public interface ISkillTypeService
    {
        Task<List<SkillTypeModel>> GetSkillTypes();
        Task<SkillTypeModel> Create(SkillTypeCreateModel model);
        Task Update(SkillTypeModel model);
    }
}
