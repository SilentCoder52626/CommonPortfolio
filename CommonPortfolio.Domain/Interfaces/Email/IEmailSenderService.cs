using CommonPortfolio.Domain.Models.Email;

namespace CommonPortfolio.Domain.Interfaces.Email
{
    public interface IEmailSenderService
    {
        void SendEmail(MessageDto message);
        Task SendEmailAsync(MessageDto message);

    }
}
