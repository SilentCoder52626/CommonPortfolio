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

            if (!ValidateDupliateTitle(model.Title)) throw new CustomException($"Skill Type with ({model.Title}) title already exists.");

            var skillType = new SkillType() { Title = model.Title,UserId = model.UserId };
            await _context.SkillTypes.AddAsync(skillType);
            await _context.SaveChangesAsync();
            tx.Complete();
            return new SkillTypeModel() { Title = model.Title, Id = skillType.Id, UserId = model.UserId };
        }

        public async Task<List<SkillTypeModel>> GetSkillTypes()
        {
            return await _context.SkillTypes.Select(x => new SkillTypeModel()
            { Title = x.Title, Id = x.Id, UserId = x.UserId }).ToListAsync();
        }

        public async Task Update(SkillTypeUpdateModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            var skillType = await _context.SkillTypes.FirstOrDefaultAsync(c=>c.Id == model.Id);
            if (skillType == null) throw new CustomException("Skill Type not found");

            if (!ValidateDupliateTitle(model.Title, skillType)) throw new CustomException($"Skill Type with ({model.Title}) title already exists.");

            skillType.Title = model.Title;
            _context.SkillTypes.Update(skillType);

            await _context.SaveChangesAsync();

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
