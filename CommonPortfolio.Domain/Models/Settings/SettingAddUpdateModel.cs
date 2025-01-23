
namespace CommonPortfolio.Domain.Models.Settings
{
    public class SettingAddUpdateModel
    {
        public Guid UserId { get; set; }
        public required string Theme { get; set; }
        public string? WEB3FormsAcessKey { get; set; }

    }
}
