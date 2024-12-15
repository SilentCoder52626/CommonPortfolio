using CommonPortfolio.Domain.Models.AccountDetails;

namespace CommonPortfolio.Domain.Interfaces
{
    public interface IAccountDetailsService
    {
        Task<List<AccountDetailsModel>> GetAccountDetails(Guid userId);
        Task<AccountDetailsModel> Create(AccountDetailsCreateModel model);
        Task Update(AccountDetailsUpdateModel model);
        Task Delete(Guid id);
    }
}
