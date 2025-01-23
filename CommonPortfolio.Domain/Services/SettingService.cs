using CommonPortfolio.Domain.Entity;
using CommonPortfolio.Domain.Helper;
using CommonPortfolio.Domain.Interfaces;
using CommonPortfolio.Domain.Interfaces.Context;
using CommonPortfolio.Domain.Models.AccountDetails;
using CommonPortfolio.Domain.Models.Settings;
using Microsoft.EntityFrameworkCore;

namespace CommonPortfolio.Domain.Services
{
    public class SettingService : ISettingService
    {
        private readonly IDBContext _context;

        public SettingService(IDBContext context)
        {
            _context = context;
        }

        public async Task<SettingModel> AddOrUpdate(SettingAddUpdateModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var settingDb = _context.Settings.Include(c => c.User).FirstOrDefault(c => c.UserId == model.UserId);

            if (settingDb == null)
            {
                settingDb = new Settings()
                {
                    Theme = model.Theme,
                    WEB3FormsAcessKey = model.WEB3FormsAcessKey,
                    UserId = model.UserId,
                };
                await _context.Settings.AddAsync(settingDb);
            }
            else
            {
                
                settingDb.Theme = model.Theme;
                settingDb.WEB3FormsAcessKey = model.WEB3FormsAcessKey;
            }
            await _context.SaveChangesAsync();
            tx.Complete();
            return new SettingModel()
            {
                Theme = settingDb.Theme,
                WEB3FormsAcessKey = settingDb.WEB3FormsAcessKey,
                Id = settingDb.Id,
                UserId = settingDb.UserId
            };
        }

        public async Task<List<SettingModel>> GetSettings(Guid userId)
        {
            var settings = await _context.Settings.Where(s => s.UserId == userId).ToListAsync();
            return settings.Select(settings => new SettingModel
            {
                Id = settings.Id,
                UserId = settings.UserId,
                Theme = settings.Theme,
                WEB3FormsAcessKey = settings.WEB3FormsAcessKey
            }).ToList();
        }
    }
}
