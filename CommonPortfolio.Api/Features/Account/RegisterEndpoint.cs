using CommonPortfolio.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CommonPortfolio.Api.Features.Account
{
    public class RegisterEndpoint : Endpoint<UserCreateModel, UserReponseModel>
    {
        private readonly IUserService _userService;

        public RegisterEndpoint(IUserService userService)
        {
            _userService = userService;
        }
        public override void Configure()
        {
            Post("auth/register");
            AllowAnonymous();
        }
        public override async Task<UserReponseModel> ExecuteAsync(UserCreateModel req, CancellationToken ct)
        {
            var response = await _userService.Create(req);
            return response;
        }
    }

}
