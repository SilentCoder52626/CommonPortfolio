using CommonPortfolio.Domain.Models.AccountLinks;

namespace CommonPortfolio.Domain.Interfaces
{
    public interface IAccountLinksService
    {
        Task<List<AccountLinksModel>> GetAccountLinks();
        Task<AccountLinksModel> Create(AccountLinksCreateModel model);
        Task Update(AccountLinksUpdateModel model);
        Task Delete(Guid id);

    }
}
