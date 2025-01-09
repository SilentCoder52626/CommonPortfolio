using CommonPortfolio.Domain.Entity;
using CommonPortfolio.Domain.Exceptions;
using CommonPortfolio.Domain.Helper;
using CommonPortfolio.Domain.Helper.CloudinaryHelper;
using CommonPortfolio.Domain.Interfaces;
using CommonPortfolio.Domain.Interfaces.Context;
using CommonPortfolio.Domain.Models.AccountDetails;
using Microsoft.EntityFrameworkCore;

namespace CommonPortfolio.Domain.Services
{
    public class AccountDetailsService : IAccountDetailsService
    {
        private readonly IDBContext _context;
        private readonly IPhotoAccessor _photoAccessor;

        public AccountDetailsService(IDBContext context, IPhotoAccessor photoAccessor)
        {
            _context = context;
            _photoAccessor = photoAccessor;
        }

        public async Task<AccountDetailsModel> AddOrUpdate(AccountDetailsAddUpdateModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var accountDetailsDb = _context.AccountDetails.Include(c=>c.User).FirstOrDefault(c => c.UserId == model.UserId);

            string bannerPictureLink = model.BannerPicture == null ? "" : (await _photoAccessor.AddPhoto(model.BannerPicture)).Url;
            string profilePictureLink = model.ProfilePicture == null ? "" : (await _photoAccessor.AddPhoto(model.ProfilePicture)).Url;
            if (accountDetailsDb == null)
            {
                accountDetailsDb = new AccountDetails()
                {
                    Position = model.Position,
                    BannerPictureLink = bannerPictureLink,
                    ProfilePictureLink = profilePictureLink,
                    ShortDescription = model.ShortDescription,
                    DetailedDescription = model.DetailedDescription,
                    SubName = model.SubName,
                    UserId = model.UserId
                };
                await _context.AccountDetails.AddAsync(accountDetailsDb);
            }
            else
            {
                var oldBannerPictureLink = accountDetailsDb.BannerPictureLink;
                var oldProfilePictureLink = accountDetailsDb.ProfilePictureLink;

                if ((!String.IsNullOrEmpty(oldBannerPictureLink) && bannerPictureLink != "") || (model.DeleteBannerPicture && !String.IsNullOrEmpty(oldBannerPictureLink)))
                {
                    await _photoAccessor.DeletePhoto(oldBannerPictureLink);
                }
                if ((!String.IsNullOrEmpty(oldProfilePictureLink) && bannerPictureLink != "") || (model.DeleteProfilePicture && !String.IsNullOrEmpty(oldProfilePictureLink)))
                {
                    await _photoAccessor.DeletePhoto(oldProfilePictureLink);
                }

                accountDetailsDb.Position = model.Position;
                accountDetailsDb.BannerPictureLink = bannerPictureLink;
                accountDetailsDb.ProfilePictureLink = profilePictureLink;
                accountDetailsDb.ShortDescription = model.ShortDescription;
                accountDetailsDb.DetailedDescription = model.DetailedDescription;
                accountDetailsDb.SubName = model.SubName;
            }
            await _context.SaveChangesAsync();
            tx.Complete();
            return new AccountDetailsModel()
            {
                Position = model.Position,
                BannerPictureLink = bannerPictureLink,
                ProfilePictureLink = profilePictureLink,
                ShortDescription = model.ShortDescription,
                DetailedDescription = model.DetailedDescription,
                SubName = model.SubName,
                Id = accountDetailsDb.Id,
                UserId = model.UserId
            };
        }

        public async Task<AccountDetailsModel> GetAccountDetails(Guid userId)
        {
            return await _context.AccountDetails.Where(c => c.UserId == userId).Select(x => new AccountDetailsModel()
            {
                Position = x.Position,
                BannerPictureLink = x.BannerPictureLink ?? "",
                ProfilePictureLink = x.ProfilePictureLink ?? "",
                ShortDescription = x.ShortDescription,
                DetailedDescription = x.DetailedDescription,
                SubName = x.SubName,
                Id = x.Id,
                UserId = x.UserId,
            }).FirstOrDefaultAsync() ?? new AccountDetailsModel();
        }

        public async Task<AccountDetails> GetById(Guid id)
        {
            var accountDetails = await _context.AccountDetails.FirstOrDefaultAsync(c => c.Id == id);
            return accountDetails == null ? throw new CustomException("Account Details not found") : accountDetails;
        }

    }
}
