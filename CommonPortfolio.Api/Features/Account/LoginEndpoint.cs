using CommonPortfolio.Domain.Helper.Hasher;
using FastEndpoints.Security;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using CommonPortfolio.Domain.Exceptions;

namespace CommonPortfolio.Api.Features.Account
{
    public class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher _hasher;

        public LoginEndpoint(IUserService userService, IPasswordHasher hasher)
        {
            _userService = userService;
            _hasher = hasher;
        }

        public override void Configure()
        {
            Post("auth/login");
            AllowAnonymous();
        }
        public override async Task<LoginResponse> ExecuteAsync(LoginRequest req, CancellationToken ct)
        {
            var user = await _userService.GetUserByUserName(req.UserName);

            if (user == null || !_hasher.ValidatePassword(req.Password,user.Password))
            {
                throw new CustomException("Incorrect username or password.");
            }

            var jwt = JwtBearer.CreateToken(options =>
            {
                options.SigningKey = Config["JWTSecret"];
                options.User.Claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
                options.User.Claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.Name));
                options.User.Claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
                options.User.Claims.Add(new Claim("UserName", user.UserName));
                options.User.Roles.Add(user.Role);
                options.ExpireAt = DateTime.UtcNow.AddDays(1);
            });

            return new LoginResponse(jwt, user.Email,user.UserName,user.Role);
        }
    }

    public record LoginRequest(string UserName, string Password);

    public record LoginResponse(string JWT, string Email, string UserName,string Role);
}
