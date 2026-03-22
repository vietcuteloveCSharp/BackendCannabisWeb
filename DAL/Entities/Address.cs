namespace DAL.Entities
{
    
    public class Address :BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }
        [Required(ErrorMessage ="Id Customer Is Required")]
        public int UserId { get; set; }
        [StringLength(150, ErrorMessage = "Country name cannot exceed 150 characters.")]
        public string Country { get; set; } = string.Empty;

		[StringLength(150, ErrorMessage = "Province name cannot exceed 150 characters.")]
        public string Province { get; set; } = string.Empty;

        [StringLength(150, ErrorMessage = "District name cannot exceed 150 characters.")]
        public string District { get; set; }= string.Empty;

		[StringLength(150, ErrorMessage = "Commune name cannot exceed 150 characters.")]
        public string Commune { get; set; }= string.Empty;

		[StringLength(150, ErrorMessage = "Road_Village_Hamlet name cannot exceed 150 characters.")]
        public string Road_Village_Hamlet { get; set; } = string.Empty;

		[StringLength(20, ErrorMessage = "HouseNumber cannot exceed 20 characters.")]
        public string HouseNumber { get; set; }= string.Empty;

		[StringLength(30, ErrorMessage = "PostalCode cannot exceed 30 characters.")]
        public string PostalCode { get; set; }= string.Empty;

		public bool IsDefault {  get; set; } =false;

        //navigation
        public virtual User User { get; set; } = default!;
       
	}
}
