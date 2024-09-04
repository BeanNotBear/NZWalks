
using System.Net;
using System.Net.Mail;

namespace NZWalks.API.Services.Implements
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration configuration;
		public EmailService(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			string MailServer = configuration["Email:MailServer"];
			string FromEmail = configuration["Email:FromEmail"];
			string Password = configuration["Email:Password"];
			int Port = int.Parse(configuration["Email:MailPort"]);

			MailMessage message = new MailMessage();
			message.From = new MailAddress(FromEmail);
			message.Subject = subject;
			message.To.Add(new MailAddress(email));
			message.Body = htmlMessage;
			message.IsBodyHtml = true;

			var smtpClient = new SmtpClient(MailServer)
			{
				Port = Port,
				Credentials = new NetworkCredential(FromEmail, Password),
				EnableSsl = true
			};

			await smtpClient.SendMailAsync(message);
		}
	}
}
