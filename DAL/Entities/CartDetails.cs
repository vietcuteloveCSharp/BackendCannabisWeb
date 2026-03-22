namespace DAL.Entities
{
    public class CartDetails :BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartDetailsId { get; set; }
        [Required(ErrorMessage = "Id cart is required.")]
        public int CartId {  get; set; }
        [Required(ErrorMessage = "Id product is required.")]
        public int ProductId { get; set; }
		public int Quantity { get; set; }
		[Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
		//navigation
		public virtual Product Product { get; set; } = default!;
        public virtual Cart Cart { get; set; } = default!;
    }
}
