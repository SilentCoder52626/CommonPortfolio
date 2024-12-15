using CommonPortfolio.Domain.Entity;
using CommonPortfolio.Domain.Exceptions;
using CommonPortfolio.Domain.Helper;
using CommonPortfolio.Domain.Helper.FileHelper;
using CommonPortfolio.Domain.Interfaces;
using CommonPortfolio.Domain.Interfaces.Context;
using CommonPortfolio.Domain.Models.AccountDetails;
using Microsoft.EntityFrameworkCore;

namespace CommonPortfolio.Domain.Services
{
    public class AccountDetailsService : IAccountDetailsService
    {
        private readonly IDBContext _context;
        private readonly IFileUploaderService _fileUploader;

        public AccountDetailsService(IDBContext context, IFileUploaderService fileUploader)
        {
            _context = context;
            _fileUploader = fileUploader;
        }

        public async Task<AccountDetailsModel> Create(AccountDetailsCreateModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var accountDetailsDb = _context.AccountDetails.Include(c=>c.User).FirstOrDefault(c => c.UserId == model.UserId);
            if(accountDetailsDb != null) throw new CustomException($"Account Details for ({accountDetailsDb.User.Name}) already exists.");

            var accountDetails = new AccountDetails()
            {
                Position = model.Position,
                BannerPictureLink = model.BannerPictureLink,
                ProfilePictureLink = model.ProfilePictureLink,
                ShortDescription = model.ShortDescription,
                DetailedDescription = model.DetailedDescription,
                SubName = model.SubName,
                UserId = model.UserId
            };
            await _context.AccountDetails.AddAsync(accountDetails);
            await _context.SaveChangesAsync();
            tx.Complete();
            return new AccountDetailsModel()
            {
                Position = model.Position,
                BannerPictureLink = String.IsNullOrEmpty(model.BannerPictureLink) ? "" : _fileUploader.GetFullFilePath(model.BannerPictureLink),
                ProfilePictureLink = String.IsNullOrEmpty(model.ProfilePictureLink) ? "" : _fileUploader.GetFullFilePath(model.ProfilePictureLink),
                ShortDescription = model.ShortDescription,
                DetailedDescription = model.DetailedDescription,
                SubName = model.SubName,
                Id = accountDetails.Id,
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
                BannerPictureLink = String.IsNullOrEmpty(x.BannerPictureLink) ? "" : _fileUploader.GetFullFilePath(x.BannerPictureLink),
                ProfilePictureLink = String.IsNullOrEmpty(x.ProfilePictureLink) ? "" : _fileUploader.GetFullFilePath(x.ProfilePictureLink),
                ShortDescription = x.ShortDescription,
                DetailedDescription = x.DetailedDescription,
                SubName = x.SubName,
                Id = x.Id,
                UserId = x.UserId,
            }).ToListAsync();
        }

        public async Task<AccountDetails> GetById(Guid id)
        {
            var accountDetails = await _context.AccountDetails.FirstOrDefaultAsync(c => c.Id == id);
            return accountDetails == null ? throw new CustomException("Account Details not found") : accountDetails;
        }

        public async Task Update(AccountDetailsUpdateModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            var accountDetails = await _context.AccountDetails.FirstOrDefaultAsync(c => c.Id == model.Id);
            if (accountDetails == null) throw new CustomException("Account Details not found");

            accountDetails.Position = model.Position;
            accountDetails.BannerPictureLink = model.BannerPictureLink;
            accountDetails.ProfilePictureLink =  model.ProfilePictureLink;
            accountDetails.ShortDescription = model.ShortDescription;
            accountDetails.DetailedDescription = model.DetailedDescription;
            accountDetails.SubName = model.SubName;

            _context.AccountDetails.Update(accountDetails);

            await _context.SaveChangesAsync();

            tx.Complete();
        }
    }
}
