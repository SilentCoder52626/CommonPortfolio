﻿using CommonPortfolio.Domain.Models.HighlightDetails;

namespace CommonPortfolio.Domain.Interfaces
{
    public interface IHighlightDetailsService
    {
        Task<List<HighlightDetailsModel>> GetHighlightDetails(Guid userId);
        Task<HighlightDetailsModel> Create(HighlightDetailsCreateModel model);
        Task Update(HighlightDetailsUpdateModel model);
        Task Delete(Guid id);

    }
}
