
using CommonPortfolio.Domain.Entity;
using CommonPortfolio.Domain.Models.User;

namespace CommonPortfolio.Domain.Interfaces
{
    public interface IUserService
    {
        Task<List<UserModel>> GetUsers(UserFilterModel filter);
        Task<UserModel> GetUser(Guid userId);
        Task<UserReponseModel> Create(UserCreateModel model);
        Task Update(UserUpdateModel model);
        Task ChangePassword(ChangePasswordModel model);

        Task<AppUser?> GetUserByEmail(string email);
        Task<AppUser?> GetUserByUserName(string userName);
        Task ResetPassword(AppUser user, string newPassword);
        Task<DetailedUserModel> GetDetailedInfo(string username);
    }
}
