using CommonPortfolio.Domain.Entity;
using CommonPortfolio.Domain.Models.AccountDetails;

namespace CommonPortfolio.Domain.Interfaces
{
    public interface IAccountDetailsService
    {
        Task<AccountDetailsModel> GetAccountDetails(Guid userId);
        Task<AccountDetailsModel> AddOrUpdate(AccountDetailsAddUpdateModel model);
        Task<AccountDetails> GetById(Guid id);
    }
}
