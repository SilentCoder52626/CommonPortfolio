using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Domain.Exceptions;

namespace CommonPortfolio.Api.Features.User
{

    public class ChangePasswordEndpoint : Endpoint<ChangePasswordRequest, ChangePasswordResponse>
    {
        private readonly IUserService _userService;

        public ChangePasswordEndpoint(IUserService userService)
        {
            _userService = userService;
        }

        public override void Configure()
        {
            Post("/api/user/change-password");
            Roles([RoleConstant.RoleUser, RoleConstant.RoleAdmin]);
        }

        public override async Task<ChangePasswordResponse> ExecuteAsync(ChangePasswordRequest req, CancellationToken ct)
        {
            await _userService.ChangePassword(new ChangePasswordModel()
            {
                NewPassword = req.NewPassword,
                OldPassword = req.OldPassword,
                Id = User.ToTokenUser().Id,
            });

            return new ChangePasswordResponse() { Message = "Password changed successfully." };
        }
    }

    public class ChangePasswordRequest
    {
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
    }

    public class ChangePasswordResponse
    {
        public required string Message { get; set; }
    }


}
