namespace DTO.DTOs.User.Users
{
	public class CreateUserDTO
	{
		[Required(ErrorMessage = "Username is required.")]
		[StringLength(100, ErrorMessage = "Username no more than 100 characters.")]
		public string Username { get; set; } = string.Empty;

		[Required(ErrorMessage = "Password is required.")]
		[RegularExpression(@"^(?=.*[A-Z]).{8,}$",
		ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự và chứa ít nhất một chữ cái viết hoa.")]
		public string Password { get; set; } = string.Empty;

		[Required(ErrorMessage = "Name is required.")]
		[StringLength(50, ErrorMessage = "Name no more than 50 characters.")]
		public string? Name { get; set; }
		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email.")]
		public string Email { get; set; } = string.Empty;
		public EUserStatus Status { get; set; } = EUserStatus.Active;
	}
}
