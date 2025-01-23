
namespace CommonPortfolio.Domain.Entity
{
    public class Settings
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        public string Theme { get; set; }
        public string WEB3FormsAcessKey { get; set; }
    }
}
