using CommonPortfolio.Domain.Entity;
using CommonPortfolio.Domain.Exceptions;
using CommonPortfolio.Domain.Helper;
using CommonPortfolio.Domain.Interfaces;
using CommonPortfolio.Domain.Interfaces.Context;
using CommonPortfolio.Domain.Models.Experience;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CommonPortfolio.Domain.Services
{
    public class ExperienceService : IExperienceService
    {
        private readonly IDBContext _context;

        public ExperienceService(IDBContext context)
        {
            _context = context;
        }

        public async Task<ExperienceModel> Create(ExperienceCreateModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            if (!ValidateDupliateTitle(model.Title)) throw new CustomException($"Experience with ({model.Title}) title already exists.");

            var experience = new Experience()
            {
                Title = model.Title,
                UserId = model.UserId,
                Duration = model.Duration,
                Organization = model.Organization,
            };
            model.ExperienceDetails.ForEach(detail =>
            {
                experience.ExperienceDetails.Add(new ExperienceDetails()
                {
                    Description = detail.Description,
                    Title = detail.Title,
                });
            });
            await _context.Experiences.AddAsync(experience);
            await _context.SaveChangesAsync();
            tx.Complete();

            return new ExperienceModel()
            {
                Title = experience.Title,
                UserId = experience.UserId,
                Duration = experience.Duration,
                Organization = experience.Organization,
                Id = experience.Id,
                ExperienceDetails = experience.ExperienceDetails.Select(detail => new ExperienceDetailsModel()
                {
                    Description = detail.Description,
                    Title = detail.Title,
                }).ToList(),
            };
        }

        public async Task Delete(Guid id)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var experience = await _context.Experiences.FirstOrDefaultAsync(c => c.Id == id);
            if (experience == null) throw new CustomException("Experience not found.");

            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();
            tx.Complete();
        }

        public async Task<List<ExperienceModel>> GetExperiences(Guid userId)
        {
            return await _context.Experiences.Where(c => c.UserId == userId).Select(experience => new ExperienceModel()
            {
                Title = experience.Title,
                UserId = experience.UserId,
                Duration = experience.Duration,
                Organization = experience.Organization,
                Id = experience.Id,
                ExperienceDetails = experience.ExperienceDetails.Select(detail => new ExperienceDetailsModel()
                {
                    Description = detail.Description,
                    Title = detail.Title,
                }).ToList(),
            }).ToListAsync();
        }

        public async Task Update(ExperienceUpdateModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            var experience = await _context.Experiences.FirstOrDefaultAsync(c => c.Id == model.Id);

            if (experience == null) throw new CustomException("Experience not found");

            if (!ValidateDupliateTitle(model.Title, experience)) throw new CustomException($"Experience with ({model.Title}) title already exists.");

            experience.Title = model.Title;
            experience.Duration = model.Duration;
            experience.Organization = model.Organization;

            if(experience.ExperienceDetails.Any())
                _context.ExperienceDetails.RemoveRange(experience.ExperienceDetails);


            model.ExperienceDetails.ForEach(detail =>
            {
                experience.ExperienceDetails.Add(new ExperienceDetails()
                {
                    Description = detail.Description,
                    Title = detail.Title,
                });
            });
            _context.Experiences.Update(experience);
            await _context.SaveChangesAsync();

            tx.Complete();
        }
        private bool ValidateDupliateTitle(string title, Experience? experience = null)
        {
            var existingType = _context.Experiences.Where(a => a.Title == title).FirstOrDefault();
            if (existingType == null || (experience != null && existingType.Id == experience.Id))
                return true;
            return false;

        }
    }
}
