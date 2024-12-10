using CommonPortfolio.Domain.Models.SkillType;
using CommonPortfolio.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonPortfolio.Domain.Interfaces
{
    public interface ISkillTypeService
    {
        Task<List<SkillTypeModel>> GetSkillTypes();
        Task<SkillTypeModel> Create(SkillTypeCreateModel model);
        Task Update(SkillTypeModel model);
    }
}
