using CommonPortfolio.Domain.Enums;

namespace CommonPortfolio.Api.Features.AccountLinks
{
    public class GetAccountLinkTypesEndpoint : EndpointWithoutRequest<List<string>>
    {
        public override void Configure()
        {
            Get("/api/account-links-types"); 
            AllowAnonymous();
        }
        public override async Task<List<string>> ExecuteAsync(CancellationToken ct)
        {
            var accountLinks = Enum.GetNames(typeof(AccountLinkEnum)).ToList();

            return await Task.FromResult(accountLinks);
        }
    }
}
