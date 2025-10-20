namespace Service.IServices.IServicesAuth
{
	public interface IForgotPasswordService
	{
		Task SendOtpAsync(string email);
		Task ForgotPasswordAsync(ResetPasswordParam resetPasswordParam);
	}
}
