namespace DAL.Entities
{
    [Table("Users", Schema = "Users")]
    public class User :BaseEntity
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, ErrorMessage = "Username no more than 100 characters.")]
        public string? Username { get; set; }
        public string? HashPassword { get; set; }
		[Required(ErrorMessage = "Name is required.")]
		[StringLength(50, ErrorMessage = "Name no more than 50 characters.")]
		public string? Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email.")]
        public string? Email { get; set; }
		[RegularExpression(@"^(0|\+84)(3[2-9]|5[6|8|9]|7[0|6-9]|8[1-5]|9[0-9])[0-9]{7}$",
		ErrorMessage = "Số điện thoại Việt Nam không hợp lệ.")]
		public string? PhoneNumber { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public EUserStatus Status { get; set; } = EUserStatus.Active;
		[Required(ErrorMessage ="Id role is required.")]
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }
        public virtual Cart? Cart { get; set; } 
		public virtual ICollection<AuditLog>? Logs { get; set; }
		// 1. Đơn hàng người dùng mua
		public ICollection<Order> OrdersAsBuyer { get; set; } = new List<Order>();

		// 2. Đơn hàng người dùng bán
		public ICollection<Order> OrdersAsSeller { get; set; } = new List<Order>();
		public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
        public virtual ICollection<Address> ? Addresses { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<AuditLog>? AuditLogs { get; set; }


	}
}
