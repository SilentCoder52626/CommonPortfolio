using CommonPortfolio.Domain.Entity;
using CommonPortfolio.Domain.Exceptions;
using CommonPortfolio.Domain.Helper;
using CommonPortfolio.Domain.Interfaces;
using CommonPortfolio.Domain.Interfaces.Context;
using CommonPortfolio.Domain.Models.AccountLinks;
using Microsoft.EntityFrameworkCore;

namespace CommonPortfolio.Domain.Services
{
    public class AccountLinksService : IAccountLinksService
    {
        private readonly IDBContext _context;

        public AccountLinksService(IDBContext context)
        {
            _context = context;
        }

        public async Task<AccountLinksModel> Create(AccountLinksCreateModel model)
        {
            try
            {
                using var tx = TransactionScopeHelper.GetInstance();
                if (!ValidateDuplicateName(model.Name)) throw new CustomException($"Account Link with ({model.Name}) name already exists.");
                var accountLink = new AccountLinks()
                {
                    Name = model.Name,
                    Url = model.Url,
                    UserId = model.UserId,
                };
                await _context.AccountLinks.AddAsync(accountLink);
                await _context.SaveChangesAsync();
                tx.Complete();

                return new AccountLinksModel()
                {
                    Id = accountLink.Id,
                    Url = accountLink.Url,
                    Name = accountLink.Name,
                    UserId = accountLink.UserId
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool ValidateDuplicateName(string name, AccountLinks? skill = null)
        {
            var existingAccountLinks = _context.AccountLinks.Where(a => a.Name == name).FirstOrDefault();
            if (existingAccountLinks == null || (skill != null && existingAccountLinks.Id == skill.Id))
                return true;
            return false;

        }

        public async Task Delete(Guid id)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            var skill = await _context.AccountLinks.FirstOrDefaultAsync(a => a.Id == id);
            if (skill == null) throw new CustomException("Account Link not found");

            _context.AccountLinks.Remove(skill);

            await _context.SaveChangesAsync();
            tx.Complete();
        }

        public async Task<List<AccountLinksModel>> GetAccountLinks(Guid userId)
        {
            return await _context.AccountLinks.Where(c=>c.UserId == userId).OrderBy(a => a.Name).Select(c => new AccountLinksModel()
            {
                Id = c.Id,
                Url = c.Url,
                Name = c.Name,
                UserId = c.UserId,
            }).ToListAsync();
        }

        public async Task Update(AccountLinksUpdateModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            var skill = await _context.AccountLinks.FirstOrDefaultAsync(a => a.Id == model.Id);
            if (skill == null) throw new CustomException("Account Links not found");

            if (!ValidateDuplicateName(model.Name,skill)) throw new CustomException($"Account Links with ({model.Name}) Name already exists.");

            skill.Name = model.Name;
            skill.Url = model.Url;

            _context.AccountLinks.Update(skill);
            await _context.SaveChangesAsync();
            tx.Complete();
        }
    }
}
