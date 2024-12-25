using CommonPortfolio.Api.Extensions;

namespace CommonPortfolio.Api.Features.User
{
    public class GetCurrentUserEndpoint : EndpointWithoutRequest<UserModel>
    {
        private readonly IUserService _userService;

        public GetCurrentUserEndpoint(IUserService userService)
        {
            _userService = userService;
        }

        public override void Configure()
        {
            Get("/api/user-details");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }

        public override async Task<UserModel> ExecuteAsync(CancellationToken ct)
        {
            var user = await _userService.GetUser(User.GetCurrentUserId());
            if(user == null)
                ThrowError("User not logged in", StatusCodes.Status401Unauthorized);
            return user;
        }
    }
}
