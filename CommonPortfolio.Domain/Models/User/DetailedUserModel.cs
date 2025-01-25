using CommonPortfolio.Domain.Models.AccountDetails;
using CommonPortfolio.Domain.Models.AccountLinks;
using CommonPortfolio.Domain.Models.Education;
using CommonPortfolio.Domain.Models.Experience;
using CommonPortfolio.Domain.Models.HighlightDetails;
using CommonPortfolio.Domain.Models.Settings;
using CommonPortfolio.Domain.Models.Skill;

namespace CommonPortfolio.Domain.Models.User
{
    public class DetailedUserModel
    {
        public UserModel User { get; set; }
        public SettingModel Setting { get; set; }
        public AccountDetailsModel AccountDetails { get; set; }
        public List<AccountLinksModel> AccountLinks { get; set; } = [];
        public List<EducationModel> Educations { get; set; } = [];
        public List<ExperienceModel> Experiences { get; set; } = [];
        public List<HighlightDetailsModel> HighlightDetails { get; set; } = [];
        public List<SkillViewModel> Skills { get; set; } = [];

    }
}
