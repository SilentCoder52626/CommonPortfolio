using CommonPortfolio.Domain.Entity;
using CommonPortfolio.Domain.Enums;
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

        public async Task<SettingModel> GetSettings(Guid userId)
        {
            return await _context.Settings.Where(c => c.UserId == userId).Select(x => new SettingModel()
            {
                Theme = x.Theme,
                WEB3FormsAcessKey = x.WEB3FormsAcessKey,
                Id = x.Id,
                UserId = x.UserId,
            }).FirstOrDefaultAsync() ?? new SettingModel() { Theme = ThemeEnum.Default.ToString(), UserId = userId };
        }
    }
}
