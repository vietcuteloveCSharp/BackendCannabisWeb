namespace DAL.Entities
{
    [Table("Reviews",Schema ="Reviews")]
    public class Review :BaseEntity
    {
        [Key]
        public int ReviewId { get; set; }
        [Required(ErrorMessage = "Id user is required.")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Id product  is required.")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Id oder  is required.")]
        public int OrderId { get; set; }
        [Range(1,5,ErrorMessage =" Rating must be from 1 to 5.")]
        public int Rating { get; set; }
        public string Comments { get; set; } = string.Empty;
        [StringLength(150,ErrorMessage = "Review title no more than 150 characters.")]
        public string ReviewTitle { get; set; }  = string.Empty;
		//navigation
		public virtual User? User { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Order? Order { get; set; }

    }
}
