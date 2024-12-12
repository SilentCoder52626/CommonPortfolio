using CommonPortfolio.Domain.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace CommonPortfolio.Api.Extensions
{
    public static class GetCurrentUser
    {
        public static TokenUser ToTokenUser(this ClaimsPrincipal user)
        {
            var userId = Guid.Parse(user.FindFirstValue(JwtRegisteredClaimNames.Sub) ?? "");
            var name = user.FindFirstValue(JwtRegisteredClaimNames.Name) ?? "";
            var email = user.FindFirstValue(JwtRegisteredClaimNames.Email) ?? "";

            return new TokenUser() { Id = userId, Name = name, Email = email };

        }
        public static Guid GetCurrentUserId(this ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if(String.IsNullOrEmpty(userId))
            {
                throw new CustomException("User not authorized!", HttpStatusCode.Unauthorized);
            }
            return Guid.Parse(userId);




        }
    }

    public class TokenUser
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}
