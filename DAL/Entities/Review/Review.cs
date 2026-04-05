
namespace DAL.Entities.Review
{
    public class Review :BaseEntity,ISoftDelete
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; } //fk user
        public int ProductId { get; set; } //fk product
        public int OrderId { get; set; } //fk order
        [Range(1,5)]
        public int Rating { get; set; }
        public string Comments { get; set; } = string.Empty;
        public string ReviewTitle { get; set; }  = string.Empty;
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }

		//navigation
		public virtual User.User User { get; set; } = default!;
        public virtual Product.Product Product { get; set; } = default!;
        public virtual Order.Order Order { get; set; } = default!;
	}
}
