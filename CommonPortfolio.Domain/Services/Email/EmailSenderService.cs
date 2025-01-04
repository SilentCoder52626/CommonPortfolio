using CommonPortfolio.Domain.Entity.Email;
using CommonPortfolio.Domain.Interfaces.Email;
using CommonPortfolio.Domain.Models.Email;
using MailKit.Net.Smtp;
using MimeKit;

namespace ServiceModule.Service.Email
{
	public class EmailSenderService : IEmailSenderService
	{
		private readonly EmailConfiguration _emailConfig;
		public EmailSenderService(EmailConfiguration emailConfig)
		{
			_emailConfig = emailConfig;
		}
		public void SendEmail(MessageDto message)
		{
			var emailMessage = CreateEmailMessage(message);
			Send(emailMessage);
		}
		private void Send(MimeMessage mailMessage)
		{
			using (var client = new SmtpClient())
			{
				try
				{
					client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
					client.AuthenticationMechanisms.Remove("XOAUTH2");
					client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
					client.Send(mailMessage);
				}
				catch
				{
					return;
				}
				finally
				{
					client.Disconnect(true);
					client.Dispose();
				}
			}
		}
		public async Task SendEmailAsync(MessageDto message)
		{
			var mailMessage = CreateEmailMessage(message);
			await SendAsync(mailMessage);
		}
		private async Task SendAsync(MimeMessage mailMessage)
		{
			using (var client = new SmtpClient())
			{
				try
				{
					await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, false);
					client.AuthenticationMechanisms.Remove("XOAUTH2");
					await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
					await client.SendAsync(mailMessage);
				}
				catch
				{
					return;
				}
				finally
				{
					await client.DisconnectAsync(true);
					client.Dispose();
				}
			}
		}
		private MimeMessage CreateEmailMessage(MessageDto message)
		{
			var emailMessage = new MimeMessage();
			emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
			emailMessage.To.AddRange(message.To);
			emailMessage.Subject = message.Subject;

			var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("{0}", message.Content) };

			if (message.Attachments != null && message.Attachments.Any())
			{
				byte[] fileBytes;
				foreach (var attachment in message.Attachments)
				{
					using (var ms = new MemoryStream())
					{
						attachment.CopyTo(ms);
						fileBytes = ms.ToArray();
					}

					bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
				}
			}

			emailMessage.Body = bodyBuilder.ToMessageBody();
			return emailMessage;
		}

	}
}
