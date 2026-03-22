namespace DAL.Entities
{
    public class Breeder :BaseEntity
    {
        [Key]
        public int BreederId { get; set; }
        [Required(ErrorMessage ="Breeder is requied.")]
        [StringLength(255 ,ErrorMessage = "Breeder name cannot exceed  255 characters.")]
        public string BreederName { get; set; } = string.Empty;
		[StringLength(150, ErrorMessage = "Country name cannot exceed 150 characters.")]
        public string Country { get; set; } = string.Empty;
		public string? Description { get; set; }
        [StringLength(255, ErrorMessage = "Website link cannot exceed 255 characters.")]
        public string? Website {  get; set; }
        public bool IsActive { get; set; }=true;
        [Required(ErrorMessage ="Email is required.")]
        [EmailAddress(ErrorMessage ="Email invalid.")]
        public string Email {  get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;

		//navigation
		public virtual ICollection<Seed> Seeds { get; set; } = new HashSet<Seed>();
    }
}
