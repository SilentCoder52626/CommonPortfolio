using CommonPortfolio.Domain.Models.Settings;


namespace CommonPortfolio.Domain.Interfaces
{
    public interface ISettingService
    {
        Task<SettingModel> GetSettings(Guid userId);
        Task<SettingModel> AddOrUpdate(SettingAddUpdateModel model);
    }
}
