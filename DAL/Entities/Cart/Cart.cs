namespace DAL.Entities.Cart
{
    public class Cart :BaseEntity ,ISoftDelete
    {
        
        public int Id { get; set; }
        
        public int UserId {  get; set; }
        public string? Session_Id { get; set; } = string.Empty; // Mã phiên làm việc

        public decimal? Price { get; set; } //khi thêm vào giỏ hàng lưu giá lại 
		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }

		//navigation 
		public virtual User.User User { get; set; } = default!;
        public virtual ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
        
    
        
    }
}
