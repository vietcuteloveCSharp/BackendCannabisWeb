namespace DAL.Entities
{
    [Table("Carts", Schema = "Orders")]
    public class Cart :BaseEntity
    {
        [Key]
        public int CartId { get; set; }
        [Required(ErrorMessage ="Id customer is required.")]
        
        public int UserId {  get; set; }
        [Required(ErrorMessage = "Id Session is required.")]
        [MaxLength(255,ErrorMessage = "Session no more than 255 characters.")]
        public string Session_Id { get; set; } = string.Empty; // Mã phiên làm việc

        [Column(TypeName ="decimal(10,2)")]
        public decimal Price { get; set; } //khi thêm vào giỏ hàng lưu giá lại 
		[Column(TypeName = "nvarchar(20)")]
		public ECartStatus Status { get; set; } = ECartStatus.Active;
		//navigation 
		public virtual User? User { get; set; }
        public virtual ICollection<CartDetails> CartDetails { get; set; } = new List<CartDetails>();
        
    
        
    }
}
