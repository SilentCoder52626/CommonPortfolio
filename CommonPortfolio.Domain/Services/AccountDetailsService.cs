using CommonPortfolio.Domain.Entity;
using CommonPortfolio.Domain.Exceptions;
using CommonPortfolio.Domain.Helper;
using CommonPortfolio.Domain.Interfaces;
using CommonPortfolio.Domain.Interfaces.Context;
using CommonPortfolio.Domain.Models.AccountDetails;
using Microsoft.EntityFrameworkCore;

namespace CommonPortfolio.Domain.Services
{
    public class AccountDetailsService : IAccountDetailsService
    {
        private readonly IDBContext _context;

        public AccountDetailsService(IDBContext context)
        {
            _context = context;
        }

        public async Task<AccountDetailsModel> Create(AccountDetailsCreateModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            var skillType = new AccountDetails()
            {
                Position = model.Position,
                BannerPictureLink = model.BannerPictureLink,
                ProfilePictureLink = model.ProfilePictureLink,
                ShortDescription = model.ShortDescription,
                DetailedDescription = model.DetailedDescription,
                SubName = model.SubName,
                UserId = model.UserId
            };
            await _context.AccountDetails.AddAsync(skillType);
            await _context.SaveChangesAsync();
            tx.Complete();
            return new AccountDetailsModel()
            {
                Position = model.Position,
                BannerPictureLink = model.BannerPictureLink,
                ProfilePictureLink = model.ProfilePictureLink,
                ShortDescription = model.ShortDescription,
                DetailedDescription = model.DetailedDescription,
                SubName = model.SubName,
                Id = skillType.Id,
                UserId = model.UserId
            };
        }

        public async Task Delete(Guid id)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var accountDetails = await _context.AccountDetails.FirstOrDefaultAsync(c => c.Id == id);
            if (accountDetails == null) throw new CustomException("Account Details not found.");

            _context.AccountDetails.Remove(accountDetails);
            await _context.SaveChangesAsync();
            tx.Complete();
        }

        public async Task<List<AccountDetailsModel>> GetAccountDetails(Guid userId)
        {
            return await _context.AccountDetails.Where(c => c.UserId == userId).Select(x => new AccountDetailsModel()
            {
                Position = x.Position,
                BannerPictureLink = x.BannerPictureLink,
                ProfilePictureLink = x.ProfilePictureLink,
                ShortDescription = x.ShortDescription,
                DetailedDescription = x.DetailedDescription,
                SubName = x.SubName,
                Id = x.Id,
                UserId = x.UserId,
            }).ToListAsync();
        }

        public async Task Update(AccountDetailsUpdateModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            var accountDetails = await _context.AccountDetails.FirstOrDefaultAsync(c => c.Id == model.Id);
            if (accountDetails == null) throw new CustomException("Account Details not found");

            accountDetails.Position = model.Position;
            accountDetails.BannerPictureLink = model.BannerPictureLink;
            accountDetails.ProfilePictureLink = model.ProfilePictureLink;
            accountDetails.ShortDescription = model.ShortDescription;
            accountDetails.DetailedDescription = model.DetailedDescription;
            accountDetails.SubName = model.SubName;

            _context.AccountDetails.Update(accountDetails);

            await _context.SaveChangesAsync();

            tx.Complete();
        }
    }
}
