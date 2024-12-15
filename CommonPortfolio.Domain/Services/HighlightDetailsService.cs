using CommonPortfolio.Domain.Entity;
using CommonPortfolio.Domain.Exceptions;
using CommonPortfolio.Domain.Helper;
using CommonPortfolio.Domain.Interfaces;
using CommonPortfolio.Domain.Interfaces.Context;
using CommonPortfolio.Domain.Models.HighlightDetails;
using Microsoft.EntityFrameworkCore;

namespace CommonPortfolio.Domain.Services
{
    public class HighlightDetailsService : IHighlightDetailsService
    {
        private readonly IDBContext _context;

        public HighlightDetailsService(IDBContext context)
        {
            _context = context;
        }

        public async Task<HighlightDetailsModel> Create(HighlightDetailsCreateModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            if (!ValidateDuplicateTitle(model.Title, model.UserId)) throw new CustomException($"Highlight Details with ({model.Title}) title already exists.");
            var skill = new HighlightDetails()
            {
                Title = model.Title,
                Description = model.Description,
                UserId = model.UserId,
            };
            await _context.HighlightDetails.AddAsync(skill);
            await _context.SaveChangesAsync();
            tx.Complete();

            return new HighlightDetailsModel()
            {
                Id = skill.Id,
                Description = skill.Description,
                Title = skill.Title,
                UserId = skill.UserId
            };
        }

        private bool ValidateDuplicateTitle(string title, Guid userId ,HighlightDetails? skill = null)
        {
            var existingHighlightDetails = _context.HighlightDetails.Where(a => a.Title == title && a.UserId == userId).FirstOrDefault();
            if (existingHighlightDetails == null || (skill != null && existingHighlightDetails.Id == skill.Id))
                return true;
            return false;

        }

        public async Task Delete(Guid id)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            var skill = await _context.HighlightDetails.FirstOrDefaultAsync(a => a.Id == id);
            if (skill == null) throw new CustomException("Highlight Details not found");

            _context.HighlightDetails.Remove(skill);

            await _context.SaveChangesAsync();
            tx.Complete();
        }

        public async Task<List<HighlightDetailsModel>> GetHighlightDetails(Guid userId)
        {
            return await _context.HighlightDetails.Where(a=>a.UserId == userId).OrderBy(a => a.Title).Select(c => new HighlightDetailsModel()
            {
                Id = c.Id,
                Description = c.Description,
                Title = c.Title,
                UserId = c.UserId,
            }).ToListAsync();
        }

        public async Task Update(HighlightDetailsUpdateModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            var skill = await _context.HighlightDetails.FirstOrDefaultAsync(a => a.Id == model.Id);
            if (skill == null) throw new CustomException("Highlight Details not found");

            if (!ValidateDuplicateTitle(model.Title,skill.UserId,skill)) throw new CustomException($"Highlight Details with ({model.Title}) title already exists.");

            skill.Title = model.Title;
            skill.Description = model.Description;

            _context.HighlightDetails.Update(skill);
            await _context.SaveChangesAsync();
            tx.Complete();
        }
    }
}
