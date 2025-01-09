using CommonBoilerPlateEight.Domain.Constants;
using CommonPortfolio.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace CommonPortfolio.Api.Features.AccountDetails
{
    public class DeleteAccountDetailsEndpoint : Endpoint<DeleteAccountDetailsRequest, DeleteAccountDetailsResponse>
    {
        private readonly IAccountDetailsService _accountDetailsService;

        public DeleteAccountDetailsEndpoint(IAccountDetailsService accountDetailsService)
        {
            _accountDetailsService = accountDetailsService;
        }

        public override void Configure()
        {
            Delete("/api/account-details/delete/{Id}");
            Roles([RoleConstant.RoleAdmin, RoleConstant.RoleUser]);
        }

        public override async Task<DeleteAccountDetailsResponse> ExecuteAsync(DeleteAccountDetailsRequest req, CancellationToken ct)
        {
            var accountDetails = await _accountDetailsService.GetById(req.Id);

            await _accountDetailsService.Delete(req.Id);

            return new DeleteAccountDetailsResponse() { Message = "Account details removed successfully." };
        }
    }

    public class DeleteAccountDetailsRequest
    {
        [FromRoute]
        public Guid Id { get; set; }
    }

    public class DeleteAccountDetailsResponse
    {
        public string Message { get; set; }
    }

}
