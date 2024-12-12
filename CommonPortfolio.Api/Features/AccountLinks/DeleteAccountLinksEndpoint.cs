using CommonBoilerPlateEight.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace CommonPortfolio.Api.Features.AccountLinks
{
    public class DeleteAccountLinksEndpoint : Endpoint<DeleteAccountLinksRequest, DeleteAccountLinksResponse>
    {
        private readonly IAccountLinksService _accountLinksService;


        public DeleteAccountLinksEndpoint(IAccountLinksService accountLinksService)
        {
            _accountLinksService = accountLinksService;
        }

        public override void Configure()
        {
            Delete("/api/account-link/delete/{Id}");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }

        public override async Task<DeleteAccountLinksResponse> ExecuteAsync(DeleteAccountLinksRequest req, CancellationToken ct)
        {
            await _accountLinksService.Delete(req.Id);
            return new DeleteAccountLinksResponse() { Message = "Account Links removed successfully." };
        }
    }

    public class DeleteAccountLinksRequest
    {
        [FromRoute]
        public Guid Id { get; set; }
    }

    public class DeleteAccountLinksResponse
    {
        public string Message { get; set; }
    }

}
