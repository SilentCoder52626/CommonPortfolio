using Microsoft.AspNetCore.Http;
using MimeKit;
namespace CommonPortfolio.Domain.Models.Email
{
    public class MessageDto
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public IFormFileCollection? Attachments { get; set; }
        public MessageDto(IEnumerable<string> to, string subject, string content, IFormFileCollection? attachments = null)
        {
            To = [.. to.Select(x => new MailboxAddress("email", x))];
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }
}
