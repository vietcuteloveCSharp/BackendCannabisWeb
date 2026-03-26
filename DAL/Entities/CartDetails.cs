namespace DAL.Entities
{
    public class CartDetails :BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int CartId {  get; set; }
        public int ProductId { get; set; }
		public int Quantity { get; set; }
        public decimal Price { get; set; }
		//navigation
		public virtual Product Product { get; set; } = default!;
        public virtual Cart Cart { get; set; } = default!;
    }
}
