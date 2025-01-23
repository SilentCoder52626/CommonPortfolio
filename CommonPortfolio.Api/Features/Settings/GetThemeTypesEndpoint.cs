using CommonPortfolio.Domain.Enums;

namespace CommonPortfolio.Api.Features.Settings
{
    public class GetThemeTypesEndpoint : EndpointWithoutRequest<List<string>>
    {
        public override void Configure()
        {
            Get("/api/theme-types"); 
            AllowAnonymous();
        }
        public override async Task<List<string>> ExecuteAsync(CancellationToken ct)
        {
            var Themes = Enum.GetNames(typeof(ThemeEnum)).ToList();

            return await Task.FromResult(Themes);
        }
    }
}
