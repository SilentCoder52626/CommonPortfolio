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

            if (!ValidateDupliateTitle(model.Title, model.UserId)) throw new CustomException($"Skill Type with ({model.Title}) title already exists.");

            var skillType = new SkillType() { Title = model.Title,UserId = model.UserId };
            await _context.SkillTypes.AddAsync(skillType);
            await _context.SaveChangesAsync();
            tx.Complete();
            return new SkillTypeModel() { Title = model.Title, Id = skillType.Id, UserId = model.UserId };
        }

        public async Task Delete(Guid id)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var skillType = await _context.SkillTypes.FirstOrDefaultAsync(c => c.Id == id);
            if (skillType == null) throw new CustomException("Skill Type not found.");

            var hasSkills = _context.Skills.Count(c => c.SkillTypeId == id) > 0;
            if (hasSkills) throw new CustomException("Skill Type has existing skills, Please delete it first to continue.");

            _context.SkillTypes.Remove(skillType);
            await _context.SaveChangesAsync();
            tx.Complete();
        }

        public async Task<List<SkillTypeModel>> GetSkillTypes(Guid userId)
        {
            return await _context.SkillTypes.Where(c=>c.UserId == userId).Select(x => new SkillTypeModel()
            { Title = x.Title, Id = x.Id, UserId = x.UserId }).ToListAsync();
        }

        public async Task Update(SkillTypeUpdateModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            var skillType = await _context.SkillTypes.FirstOrDefaultAsync(c=>c.Id == model.Id);
            if (skillType == null) throw new CustomException("Skill Type not found");

            if (!ValidateDupliateTitle(model.Title,skillType.UserId, skillType)) throw new CustomException($"Skill Type with ({model.Title}) title already exists.");

            skillType.Title = model.Title;
            _context.SkillTypes.Update(skillType);

            await _context.SaveChangesAsync();

            tx.Complete();
        }
        private bool ValidateDupliateTitle(string title,Guid userId, SkillType? skillType = null)
        {
            var existingType = _context.SkillTypes.Where(a => a.Title == title && a.UserId == userId).FirstOrDefault();
            if (existingType == null || (skillType != null && existingType.Id == skillType.Id))
                return true;
            return false;

        }
    }
}
