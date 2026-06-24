namespace Shared.DTOs.Common.Params
{
	public class ResetPasswordParam
	{
		 public string Email { get; set; } =string.Empty;
		public string Otp { get; set; } = string.Empty;
		public string NewPassword { get; set; } = string.Empty;
	}
}
