using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Api.Extensions;
using CommonPortfolio.Api.Helpers;
using CommonPortfolio.Domain.Enums;
using CommonPortfolio.Domain.Exceptions;
using CommonPortfolio.Domain.Interfaces.Email;
using CommonPortfolio.Domain.Models.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommonPortfolio.Api.Features.Account
{

    public class ForgetPasswordEndpoint : Endpoint<ForgetPasswordRequest, ForgetPasswordResponse>
    {
        private readonly IUserService _userService;
        public readonly IEmailSenderService _emailService;
        public ForgetPasswordEndpoint(IUserService userService, IEmailSenderService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        public override void Configure()
        {
            Post("/auth/forget-password");
            AllowAnonymous();
        }

        public override async Task<ForgetPasswordResponse> ExecuteAsync(ForgetPasswordRequest req, CancellationToken ct)
        {
            var user = await _userService.GetUserByEmail(req.Email) ?? throw new CustomException("Uh-oh! The email address seems to be lost in the digital abyss. Double-check and make sure it's a valid email.");

            if (user.Email != null)
            {
                string newPassword = RandomAlphaNumericHelper.Random(8);

                await _userService.ResetPassword(user, newPassword).ConfigureAwait(true);

                var htmlString = $"<p>Dear {user.Name} ({user.UserName}),<br/><br/>\r\n&emsp;&emsp; Welcome back! Your password has been successfully reset. Use <b>{newPassword}</b> as your new login key to access your account.\r\n<br/>\r\n&emsp;&emsp; Remember to <b>update</b> your password for added security.\r\n<br/><br/>\r\nRegards,<br/>\r\nCommonWorld\r\n</p>";
                var message = new MessageDto(new string[] { user.Email }, "User Password Reset.", htmlString, null);
                await _emailService.SendEmailAsync(message).ConfigureAwait(true);

            }
            else
            {
                throw new CustomException("Email address of the user is invalid.");
            }

            return new ForgetPasswordResponse() { Message = "Password Reset Email Send Succussfully." };
        }
    }

    public class ForgetPasswordRequest
    {
        public required string Email { get; set; }
    }

    public class ForgetPasswordResponse
    {
        public required string Message { get; set; }
    }


}
