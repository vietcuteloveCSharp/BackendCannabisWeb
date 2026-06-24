namespace Service.Interfaces.Auth
{
	public interface IEmailService
	{
		Task SendMailAsync(EmailMessageParam message);
	}
}
