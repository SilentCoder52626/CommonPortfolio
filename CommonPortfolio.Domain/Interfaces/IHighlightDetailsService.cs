

using CommonPortfolio.Domain.Models.HighlightDetailsModel;

namespace CommonPortfolio.Domain.Interfaces
{
    public interface IHighlightDetailsService
    {
        Task<List<HighlightDetailsModel>> GetHighlightDetailss();
        Task<HighlightDetailsModel> Create(HighlightDetailsCreateModel model);
        Task Update(HighlightDetailsUpdateModel model);
        Task Delete(Guid id);

    }
}
