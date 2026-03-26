namespace DAL.Entities
{
    public class Cart :BaseEntity
    {
        [Key]
        public int Id { get; set; }
        
        public int UserId {  get; set; }
        public string Session_Id { get; set; } = string.Empty; // Mã phiên làm việc

        public decimal Price { get; set; } //khi thêm vào giỏ hàng lưu giá lại 
		public ECartStatus Status { get; set; } = ECartStatus.Active;
		//navigation 
		public virtual User User { get; set; } = default!;
        public virtual ICollection<CartDetails> CartDetails { get; set; } = new HashSet<CartDetails>();
        
    
        
    }
}
