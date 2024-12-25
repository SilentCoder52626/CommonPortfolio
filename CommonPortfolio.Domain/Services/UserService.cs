using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Domain.Entity;
using CommonPortfolio.Domain.Exceptions;
using CommonPortfolio.Domain.Helper;
using CommonPortfolio.Domain.Helper.Hasher;
using CommonPortfolio.Domain.Interfaces;
using CommonPortfolio.Domain.Interfaces.Context;
using CommonPortfolio.Domain.Models.User;
using Microsoft.EntityFrameworkCore;

namespace CommonPortfolio.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IDBContext _context;
        private readonly IPasswordHasher _hasher;

        public UserService(IDBContext context, IPasswordHasher hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        public async Task ChangePassword(ChangePasswordModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            
            var user = await _context.AppUsers.Where(a=>a.Id == model.Id).FirstOrDefaultAsync() ?? throw new CustomException("User not found.");

            if (!_hasher.ValidatePassword(model.OldPassword,user.Password))
            {
                throw new CustomException("Old password didnot match.");
            }
            user.Password = _hasher.HashPassword(model.NewPassword);

            _context.AppUsers.Update(user);
            await _context.SaveChangesAsync();
            tx.Complete();

        }

        public async Task<UserReponseModel> Create(UserCreateModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            ValidateUser(model.UserName, model.Email);

            var user = new AppUser()
            {
                UserName = model.UserName,
                Name = model.Name,
                Email = model.Email,
                Contact = model.Contact,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
                Password = _hasher.HashPassword(model.Password),
                Role = RoleConstant.RoleUser
            };
            await _context.AppUsers.AddAsync(user);
            await _context.SaveChangesAsync();
            tx.Complete();

            return new UserReponseModel() { Email = user.Email, Id = user.Id, Name = user.Name, UserName = user.UserName };
        }

        private void ValidateUser(string userName, string email)
        {
            if (!ValidateDupliateUserName(userName))
            {
                throw new CustomException($"User with ({userName}) username already exists.");
            }
            if (!ValidateDupliateUserEmail(email))
            {
                throw new CustomException($"User with ({email}) email already exists.");
            }
        }

        public async Task<List<UserModel>> GetUsers(UserFilterModel filter)
        {
            var userQueryable = _context.AppUsers.Where(a => a.IsDeleted == false);
            if (!String.IsNullOrEmpty(filter.Name))
            {
                userQueryable = userQueryable.Where(a=>a.Name.Contains(filter.Name));
            }
            if (!String.IsNullOrEmpty(filter.Email))
            {
                userQueryable = userQueryable.Where(a=> a.Email.ToLower() == filter.Email.ToLower());
            }
            if (!String.IsNullOrEmpty(filter.Role))
            {
                userQueryable = userQueryable.Where(a=>a.Role == filter.Role);
            }

            return await userQueryable.Where(a => a.IsDeleted == false).Select(a => new UserModel()
            {
                Contact = a.Contact,
                CreatedDate = a.CreatedDate,
                Email = a.Email,
                Id = a.Id,
                Name = a.Name,
                Role = a.Role,
            }).ToListAsync();
        }
        public async Task<UserModel?> GetUser(Guid userId)
        {
            return await _context.AppUsers.Where(a => a.IsDeleted == false && a.Id == userId ).Select(a => new UserModel()
            {
                Contact = a.Contact,
                CreatedDate = a.CreatedDate,
                Email = a.Email,
                Id = a.Id,
                Name = a.Name,
                Role = a.Role,
            }).FirstOrDefaultAsync() ;
        }

        public async Task Update(UserUpdateModel model)
        {
            using var tx = TransactionScopeHelper.GetInstance();


            var user = await _context.AppUsers.Where(a => a.Id == model.Id).FirstOrDefaultAsync() ?? throw new CustomException("User not found.");

            if(!ValidateDupliateUserEmail(model.Email,user))
            {
                throw new CustomException($"User with ({model.Email}) email already exists.");
            }

            user.Name = model.Name;
            user.Email = model.Email;
            user.Contact = model.Contact;

            _context.AppUsers.Update(user);
            await _context.SaveChangesAsync();
            tx.Complete();
        }
        public async Task<AppUser?> GetUserByEmail(string email)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(a => a.Email.ToLower() == email.ToLower());
        }
        public async Task<AppUser?> GetUserByUserName(string userName)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(a => a.UserName == userName);
        }
        private bool ValidateDupliateUserEmail(string email, AppUser? user = null)
        {
            var existingUser = _context.AppUsers.Where(a=> a.Email.ToLower() == email.ToLower()).FirstOrDefault();
            if(existingUser == null || (user != null && existingUser.Id == user.Id))
                return true;
            return false;

        }
        private bool ValidateDupliateUserName(string userName, AppUser? user = null)
        {
            var existingUser = _context.AppUsers.Where(a=>a.UserName == userName).FirstOrDefault();
            if(existingUser == null || (user != null && existingUser.Id == user.Id))
                return true;
            return false;

        }
    }
}
