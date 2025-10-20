using DTO.Params;
using Microsoft.Extensions.Configuration;

namespace TestsCannabis.TestService.MailServiceTests
{
	public class EmailServiceTests_SendMail
	{
		private static EmailService CreateService(MailSettings settings)
		=> new EmailService(Options.Create(settings));


		[Fact]
		public async Task SendMailAsync_ShouldThrow_WhenConfigMissing()
		{
			// Arrange
			var service = CreateService(new MailSettings());
			var message = new EmailMessageParam
			{
				To = "test@test.com",
				Subject = "No Config",
				Body = "Body"
			};

			// Act + Assert
			await Assert.ThrowsAsync<InvalidOperationException>(() => service.SendMailAsync(message));
		}

		[Fact]
		public async Task SendMailAsync_ShouldThrow_WhenHostInvalid()
		{
			var settings = new MailSettings
			{
				From = "noreply@local.dev",
				DisplayName = "TestMailer",
				Smtp = new SmtpSettings
				{
					Host = "invalid-host",
					Port = 25,
					Username = "user",
					Password = "pass",
					UseSsl = false
				}
			};
			var service = CreateService(settings);
			var message = new EmailMessageParam
			{
				To = "receiver@test.com",
				Subject = "Bad Host",
				Body = "..."
			};
			await Assert.ThrowsAsync<System.Net.Sockets.SocketException>(() => service.SendMailAsync(message));
		}

		[Fact]
		public async Task SendMailAsync_ShouldSend_WhenValidConfig()
		{
			var settings = new MailSettings
			{
				From = "noreply@local.dev",
				DisplayName = "TestMailer",
				Smtp = new SmtpSettings
				{
					Host = "localhost",
					Port = 25,
					Username = "user",
					Password = "pass",
					UseSsl = false
				}
			};
			var service = CreateService(settings);
			var message = new EmailMessageParam
			{
				To = "receiver@test.com",
				Subject = "OK",
				Body = "<b>Works!</b>"
			};

			await service.SendMailAsync(message);

			Assert.True(true);
		}
	}

}

