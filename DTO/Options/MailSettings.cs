namespace DTO.Options
{
	public class MailSettings
	{
		public string From { get; set; } = string.Empty;
		public string DisplayName { get; set; } = string.Empty;
		public SmtpSettings Smtp { get; set; } = new SmtpSettings();
	}
}
