using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Domain.Models.Settings;

namespace CommonPortfolio.Api.Features.Settings
{
    public class CreateSettingsEndpoint : Endpoint<SettingsAddUpdateRequestModel, SettingModel>
    {
        private readonly ISettingService _settingsService;
        public CreateSettingsEndpoint(ISettingService SettingsService)
        {
            _settingsService = SettingsService;
        }

        public override void Configure()
        {
            Post("/api/settings/addorupdate");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }

        public override async Task<SettingModel> ExecuteAsync(SettingsAddUpdateRequestModel req, CancellationToken ct)
        {
            var createModel = new SettingAddUpdateModel
            {
                UserId = User.GetCurrentUserId(),
                Theme = req.Theme,
                WEB3FormsAcessKey = req.WEB3FormsAcessKey
            };

            return await _settingsService.AddOrUpdate(createModel);
        }

    }
    public class SettingsAddUpdateRequestModel
    {
        public required string Theme { get; set; }
        public string? WEB3FormsAcessKey { get; set; }
    }

}
