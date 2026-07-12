using DAL.Entities.Inherited;

namespace DAL.Entities.Shop
{
    public class Address :BaseEntity , ISoftDelete
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; } //Fk
        public string Country { get; set; } = string.Empty;
        public string City { get; set; }  =string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Ward { get; set; } = string.Empty;

		public string HouseNumber { get; set; }= string.Empty;
        public bool IsDefault { get; set; } = false;
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }
		//navigation
		public virtual Customer Customer { get; set; } = default!;
       
	}
}
