namespace DAL.Entities
{
    [Table("OrderItems",Schema = "Orders")]
    public class OrderItem :BaseEntity
    {
        [Key]
        public int OrderItemId { get; set; }
        [Required(ErrorMessage = "Id oder is required.")]
        public int OrderId {  get; set; }
        [Required(ErrorMessage = "Id product is required.")]
        public int ProductId {  get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
		public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
