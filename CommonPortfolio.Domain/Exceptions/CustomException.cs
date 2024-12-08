using System.Net;

namespace CommonPortfolio.Domain.Exceptions
{
    public class CustomException : BaseException
    {
        public CustomException(string message) : base(message)
        {
            StatusCode = System.Net.HttpStatusCode.BadRequest;
        }
        public CustomException(string message,HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
