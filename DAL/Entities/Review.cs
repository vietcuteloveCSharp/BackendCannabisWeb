namespace DAL.Entities
{
    public class Review :BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; } = string.Empty;
        public string ReviewTitle { get; set; }  = string.Empty;
		//navigation
		public virtual User User { get; set; } = default!;
        public virtual Product Product { get; set; } = default!;
        public virtual Order Order { get; set; } = default!;

    }
}
