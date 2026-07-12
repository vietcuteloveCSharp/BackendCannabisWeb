namespace Service.Interfaces.Auth.Internal
{
	public interface IEmailService
	{
		Task SendMailAsync(EmailMessageParam message);
	}
}
