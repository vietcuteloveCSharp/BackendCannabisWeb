namespace Service.IServices.IServicesAuth
{
	public interface IEmailService
	{
		Task SendMailAsync(EmailMessageParam message);
	}
}
