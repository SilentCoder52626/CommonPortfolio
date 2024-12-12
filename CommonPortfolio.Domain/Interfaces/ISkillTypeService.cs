using CommonPortfolio.Domain.Models.SkillType;

namespace CommonPortfolio.Domain.Interfaces
{
    public interface ISkillTypeService
    {
        Task<List<SkillTypeModel>> GetSkillTypes(Guid userId);
        Task<SkillTypeModel> Create(SkillTypeCreateModel model);
        Task Update(SkillTypeUpdateModel model);
        Task Delete(Guid id);
    }
}
