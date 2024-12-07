
namespace CommonPortfolio.Api.Features
{
    public class TestFastEndpoint : EndpointWithoutRequest<TestFastResponse>
    {
        public override void Configure()
        {
            Get("/api/test");
            AllowAnonymous();
        }

        public override async Task<TestFastResponse> ExecuteAsync(CancellationToken ct)
        {
            var response = new TestFastResponse()
            {
                Message = "This is test message."
            };
            return await Task.FromResult(response);
        }
    }

    public class TestFastResponse
    {
        public string Message { get; set; }
    }

}
