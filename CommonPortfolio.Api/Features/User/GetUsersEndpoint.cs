using CommonBoilerPlateEight.Domain.Constants;

namespace CommonPortfolio.Api.Features.User
{
    public class GetUsersEndpoint : Endpoint<UserFilterModel, List<UserModel>>
    {
        private readonly IUserService _userService;

        public GetUsersEndpoint(IUserService userService)
        {
            _userService = userService;
        }

        public override void Configure()
        {
            Get("/api/users");
            Roles(RoleConstant.RoleAdmin);
        }

        public override async Task<List<UserModel>> ExecuteAsync(UserFilterModel req, CancellationToken ct)
        {
            return await _userService.GetUsers(req);

        }
    }
}
