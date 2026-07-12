using Service.Interfaces.Auth.Internal;
using Shared.DTOs.Common.Params;

namespace TestsCannabis.Mocks
{
	public class FakeEmailService : IEmailService
	{
		public Task SendMailAsync(EmailMessageParam message)
		{
			return Task.CompletedTask;
		}
	}
}
