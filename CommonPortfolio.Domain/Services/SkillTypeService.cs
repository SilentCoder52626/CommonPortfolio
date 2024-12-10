using CommonPortfolio.Domain.Entity;
using CommonPortfolio.Domain.Exceptions;
using CommonPortfolio.Domain.Helper;
using CommonPortfolio.Domain.Interfaces;
using CommonPortfolio.Domain.Interfaces.Context;
using CommonPortfolio.Domain.Models.SkillType;
using Microsoft.EntityFrameworkCore;

namespace CommonPortfolio.Domain.Services
{
    public class SkillTypeService : ISkillTypeService
    {
        private readonly IDBContext _context;

        public SkillTypeService(IDBContext context)
        {
            _context = context;
        }

        public async Task<SkillTypeModel> Create(SkillTypeCreateModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            ValidateDupliateTitle(model.Title);

            var skillType = new SkillType() { Title = model.Title };
            await _context.SkillTypes.AddAsync(skillType);
            tx.Complete();
            return new SkillTypeModel() { Title = model.Title, Id = skillType.Id };
        }

        public async Task<List<SkillTypeModel>> GetSkillTypes()
        {
            return await _context.SkillTypes.Select(x => new SkillTypeModel()
            { Title = x.Title, Id = x.Id }).ToListAsync();
        }

        public async Task Update(SkillTypeModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            var skillType = await _context.SkillTypes.FirstOrDefaultAsync(c=>c.Id == model.Id);
            if (skillType == null) throw new CustomException("Skill Type not found");

            ValidateDupliateTitle(model.Title, skillType);

            skillType.Title = model.Title;
            _context.SkillTypes.Update(skillType);
            tx.Complete();
        }
        private bool ValidateDupliateTitle(string title, SkillType? skillType = null)
        {
            var existingType = _context.SkillTypes.Where(a => a.Title == title).FirstOrDefault();
            if (existingType == null || (skillType != null && existingType.Id == skillType.Id))
                return true;
            return false;

        }
    }
}
