﻿namespace CommonPortfolio.Domain.Models.Education
{
    public class EducationUpdateModel
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string University { get; set; }
        public required string Address { get; set; }
        public required string Description { get; set; }
        public required int StartYear { get; set; }
        public int? EndYear { get; set; }
    }
}

