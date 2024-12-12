using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Api.Extensions;

namespace CommonPortfolio.Api.Features.User
{
    public class UpdateUserEndpoint : Endpoint<UpdateUserRequest, UpdateUserResponse>
    {
        private readonly IUserService _userService;

        public UpdateUserEndpoint(IUserService userService)
        {
            _userService = userService;
        }
        public override void Configure()
        {
            Post("api/user/update");
            Roles([RoleConstant.RoleAdmin,RoleConstant.RoleUser]);
        }
        public override async Task<UpdateUserResponse> ExecuteAsync(UpdateUserRequest req, CancellationToken ct)
        {
            await _userService.Update(new UserUpdateModel()
            {
                Contact = req.Contact,
                Email = req.Email,
                Name = req.Name,
                Id = User.GetCurrentUserId(),
            });
            return new UpdateUserResponse() { Message = "User Updated Successfully." };
        }

    }
    public class UpdateUserRequest
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Contact { get; set; }
    }
    public class UpdateUserResponse
    {
        public required string Message { get; set; }
    }
}
