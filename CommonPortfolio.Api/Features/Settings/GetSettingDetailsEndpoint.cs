using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Domain.Models.Settings;

namespace CommonPortfolio.Api.Features.Settings
{
    public class GetSettingsEndpoint : EndpointWithoutRequest<SettingModel>
    {
        private readonly ISettingService _settingService;

        public GetSettingsEndpoint(ISettingService acccountDetailsService)
        {
            _settingService = acccountDetailsService;
        }

        public override void Configure()
        {
            Get("/api/settings");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }
        public override async Task<SettingModel> ExecuteAsync(CancellationToken ct)
        {
            return await _settingService.GetSettings(User.GetCurrentUserId());
        }
    }


}
