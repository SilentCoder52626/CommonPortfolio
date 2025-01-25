using CommonPortfolio.Domain.Entity;
using CommonPortfolio.Domain.Exceptions;
using CommonPortfolio.Domain.Helper;
using CommonPortfolio.Domain.Helper.CloudinaryHelper;
using CommonPortfolio.Domain.Interfaces;
using CommonPortfolio.Domain.Interfaces.Context;
using CommonPortfolio.Domain.Models.AccountDetails;
using CommonPortfolio.Domain.Models.Cloudinary;
using Microsoft.AspNetCore.Components.Routing;
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

            var bannerPictureLink = model.BannerPicture == null ? new PhotoUploadResult() : (await _photoAccessor.AddPhoto(model.BannerPicture));
            var profilePictureLink = model.ProfilePicture == null ? new PhotoUploadResult() : (await _photoAccessor.AddPhoto(model.ProfilePicture));
            if (accountDetailsDb == null)
            {
                accountDetailsDb = new AccountDetails()
                {
                    Position = model.Position,
                    BannerPictureLink = bannerPictureLink?.Url,
                    ProfilePictureLink = profilePictureLink?.Url,
                    ShortDescription = model.ShortDescription,
                    DetailedDescription = model.DetailedDescription,
                    SubName = model.SubName,
                    CVLink = model.CVLink,
                    UserId = model.UserId,
                    ProfilePicturePublicId = profilePictureLink?.PublicId,
                    BannerPicturePublicId = bannerPictureLink?.PublicId
                };
                await _context.AccountDetails.AddAsync(accountDetailsDb);
            }
            else
            {
                var oldBannerPicturePublicId = accountDetailsDb.BannerPicturePublicId;
                var oldProfilePicturePublicId = accountDetailsDb.ProfilePicturePublicId;

                if ((!String.IsNullOrEmpty(oldBannerPicturePublicId) && !String.IsNullOrEmpty(bannerPictureLink?.Url)) || (model.DeleteBannerPicture && !String.IsNullOrEmpty(oldBannerPicturePublicId)))
                {
                    if(await _photoAccessor.DeletePhoto(oldBannerPicturePublicId))
                    {
                        accountDetailsDb.BannerPictureLink = null;
                        accountDetailsDb.BannerPicturePublicId = null;
                    }
                }
                if ((!String.IsNullOrEmpty(oldProfilePicturePublicId) && !String.IsNullOrEmpty(profilePictureLink?.Url)) || (model.DeleteProfilePicture && !String.IsNullOrEmpty(oldProfilePicturePublicId)))
                {
                    if (await _photoAccessor.DeletePhoto(oldProfilePicturePublicId))
                    {
                        accountDetailsDb.ProfilePictureLink = null;
                        accountDetailsDb.ProfilePicturePublicId = null;
                    }
                }

                accountDetailsDb.Position = model.Position;
                accountDetailsDb.BannerPictureLink = bannerPictureLink?.Url ?? accountDetailsDb.BannerPictureLink;
                accountDetailsDb.ProfilePictureLink = profilePictureLink?.Url ?? accountDetailsDb.ProfilePictureLink;
                accountDetailsDb.ShortDescription = model.ShortDescription;
                accountDetailsDb.DetailedDescription = model.DetailedDescription;
                accountDetailsDb.ProfilePicturePublicId = profilePictureLink?.PublicId ?? accountDetailsDb.ProfilePicturePublicId;
                accountDetailsDb.BannerPicturePublicId = bannerPictureLink?.PublicId ?? accountDetailsDb.BannerPicturePublicId;
                accountDetailsDb.SubName = model.SubName;
                accountDetailsDb.CVLink = model.CVLink;
            }
            await _context.SaveChangesAsync();
            tx.Complete();
            return new AccountDetailsModel()
            {
                Position = model.Position,
                BannerPictureLink = accountDetailsDb.BannerPictureLink,
                ProfilePictureLink = accountDetailsDb.ProfilePictureLink,
                ShortDescription = accountDetailsDb.ShortDescription,
                DetailedDescription = accountDetailsDb.DetailedDescription,
                SubName = accountDetailsDb.SubName,
                CVLink = accountDetailsDb.CVLink,
                Id = accountDetailsDb.Id,
                UserId = accountDetailsDb.UserId
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
                CVLink = x.CVLink,
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
