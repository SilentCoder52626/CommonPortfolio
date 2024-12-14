using CommonPortfolio.Domain.Entity;
using CommonPortfolio.Domain.Exceptions;
using CommonPortfolio.Domain.Helper;
using CommonPortfolio.Domain.Interfaces;
using CommonPortfolio.Domain.Interfaces.Context;
using CommonPortfolio.Domain.Models.Education;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CommonPortfolio.Domain.Services
{
    public class EducationService : IEducationService
    {
        private readonly IDBContext _context;

        public EducationService(IDBContext context)
        {
            _context = context;
        }

        public async Task<EducationModel> Create(EducationCreateModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            if (!ValidateDupliateTitle(model.Title)) throw new CustomException($"Education with ({model.Title}) title already exists.");

            var education = new Education()
            {
                Title = model.Title,
                UserId = model.UserId,
                Address = model.Address,
                Description = model.Description,
                EndYear = model.EndYear,
                StartYear = model.StartYear,
                University = model.University,
            };
            await _context.Educations.AddAsync(education);
            await _context.SaveChangesAsync();
            tx.Complete();

            return new EducationModel()
            {
                Title = education.Title,
                UserId = education.UserId,
                Address = education.Address,
                Description = education.Description,
                EndYear = education.EndYear,
                StartYear = education.StartYear,
                University = education.University,
                Id = education.Id,
            };
        }

        public async Task Delete(Guid id)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var education = await _context.Educations.FirstOrDefaultAsync(c => c.Id == id);
            if (education == null) throw new CustomException("Education not found.");

            _context.Educations.Remove(education);
            await _context.SaveChangesAsync();
            tx.Complete();
        }

        public async Task<List<EducationModel>> GetEducations(Guid userId)
        {
            return await _context.Educations.Where(c => c.UserId == userId).Select(education => new EducationModel()
            {
                Title = education.Title,
                UserId = education.UserId,
                Address = education.Address,
                Description = education.Description,
                EndYear = education.EndYear,
                StartYear = education.StartYear,
                University = education.University,
                Id = education.Id,
            }).ToListAsync();
        }

        public async Task Update(EducationUpdateModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            var education = await _context.Educations.FirstOrDefaultAsync(c => c.Id == model.Id);

            if (education == null) throw new CustomException("Education not found");

            if (!ValidateDupliateTitle(model.Title, education)) throw new CustomException($"Education with ({model.Title}) title already exists.");

            education.Title = model.Title;
            education.Address = model.Address;
            education.Description = model.Description;
            education.EndYear = model.EndYear;
            education.StartYear = model.StartYear;
            education.University = model.University;

            _context.Educations.Update(education);
            await _context.SaveChangesAsync();

            tx.Complete();
        }
        private bool ValidateDupliateTitle(string title, Education? education = null)
        {
            var existingType = _context.Educations.Where(a => a.Title == title).FirstOrDefault();
            if (existingType == null || (education != null && existingType.Id == education.Id))
                return true;
            return false;

        }
    }
}
