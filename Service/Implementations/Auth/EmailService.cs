namespace Service.Implementations.Auth
{
	public class EmailService : IEmailService
	{
		private readonly MailSettings _mailSettings;
		public EmailService(IOptions<MailSettings> mailSettings)
		{
			_mailSettings = mailSettings.Value ?? throw new ArgumentNullException(nameof(mailSettings), "Mail settings cannot be null.");


		}
		public async Task SendMailAsync(EmailMessageParam param)
		{
			var email = new MimeMessage();
			email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.From));
			email.To.Add(MailboxAddress.Parse(param.To));
			email.Subject = param.Subject;

			var builder = new BodyBuilder
			{
				HtmlBody = param.Body
			};
			email.Body = builder.ToMessageBody();

			using var smtp = new SmtpClient();
			await smtp.ConnectAsync(
				_mailSettings.Smtp.Host,
				_mailSettings.Smtp.Port,
				_mailSettings.Smtp.UseSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls
			);

			await smtp.AuthenticateAsync(_mailSettings.Smtp.Username, _mailSettings.Smtp.Password);
			await smtp.SendAsync(email);
			await smtp.DisconnectAsync(true);
		}
	}
}
