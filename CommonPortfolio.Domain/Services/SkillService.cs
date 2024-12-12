using CommonPortfolio.Domain.Entity;
using CommonPortfolio.Domain.Exceptions;
using CommonPortfolio.Domain.Helper;
using CommonPortfolio.Domain.Interfaces;
using CommonPortfolio.Domain.Interfaces.Context;
using CommonPortfolio.Domain.Models.Skill;
using Microsoft.EntityFrameworkCore;

namespace CommonPortfolio.Domain.Services
{
    public class SkillService : ISkillService
    {
        private readonly IDBContext _context;

        public SkillService(IDBContext context)
        {
            _context = context;
        }

        public async Task<SkillModel> Create(SkillCreateModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            if (!ValidateDuplicateTitle(model.Title)) throw new CustomException($"Skill with ({model.Title}) title already exists.");
            var skill = new Skill()
            {
                Title = model.Title,
                SkillTypeId = model.SkillTypeId,
                UserId = model.UserId,
            };
            await _context.Skills.AddAsync(skill);
            await _context.SaveChangesAsync();
            tx.Complete();

            return new SkillModel()
            {
                Id = skill.Id,
                SkillTypeId = skill.SkillTypeId,
                Title = skill.Title,
                UserId = skill.UserId
            };
        }

        private bool ValidateDuplicateTitle(string title, Skill? skill = null)
        {
            var existingSkill = _context.Skills.Where(a => a.Title == title).FirstOrDefault();
            if (existingSkill == null || (skill != null && existingSkill.Id == skill.Id))
                return true;
            return false;

        }

        public async Task Delete(Guid id)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            var skill = await _context.Skills.FirstOrDefaultAsync(a => a.Id == id);
            if (skill == null) throw new CustomException("Skill not found");

            _context.Skills.Remove(skill);

            await _context.SaveChangesAsync();
            tx.Complete();
        }

        public async Task<List<SkillModel>> GetSkills(Guid userId)
        {
            return await _context.Skills.Where(a=>a.UserId == userId).OrderBy(a => a.Title).Select(c => new SkillModel()
            {
                Id = c.Id,
                SkillTypeId = c.SkillTypeId,
                Title = c.Title,
                UserId = c.UserId,
            }).ToListAsync();
        }

        public async Task Update(SkillUpdateModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            var skill = await _context.Skills.FirstOrDefaultAsync(a => a.Id == model.Id);
            if (skill == null) throw new CustomException("Skill not found");

            if (!ValidateDuplicateTitle(model.Title,skill)) throw new CustomException($"Skill with ({model.Title}) title already exists.");

            skill.Title = model.Title;
            skill.SkillTypeId = model.SkillTypeId;

            _context.Skills.Update(skill);
            await _context.SaveChangesAsync();
            tx.Complete();
        }
    }
}
