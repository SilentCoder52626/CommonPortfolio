
using Microsoft.AspNetCore.Mvc;

namespace CommonPortfolio.Api.Features.User
{
    public class GetDetailedInfoEndpoint : Endpoint<GetDetailedInfoRequestModel, DetailedUserModel>
    {
        private readonly IUserService _userService;

        public GetDetailedInfoEndpoint(IUserService userService)
        {
            _userService = userService;
        }

        public override void Configure()
        {
            Get("/api/{username}/info");
            AllowAnonymous();

        }

        public override async Task<DetailedUserModel> ExecuteAsync(GetDetailedInfoRequestModel req, CancellationToken ct)
        {
            return await _userService.GetDetailedInfo(req.username);
        }
    }
    public class GetDetailedInfoRequestModel
    {
        [FromRoute]
        public required string username { get; set; }


    }
}
