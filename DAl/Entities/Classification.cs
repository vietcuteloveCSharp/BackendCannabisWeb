namespace DAL.Entities
{
    [Table("Classifies")]   
    public class Classification : BaseEntity
    {
       
        [Key]
        public int ClassificationId { get; set; }
        [Required(ErrorMessage = "ClassificationName is required.")]
        [StringLength(150,ErrorMessage = "ClassificationName no than more 150 characters.")]
        public string ClassificationName { get; set; } = string.Empty;
		public int Quantity { get; set; }
        public string? Description { get; set; }
        public bool IsActive {  get; set; } =true;
        //navigation

        public virtual ICollection<Seed> Seeds { get; set; }  = new List<Seed>();
	}
}
