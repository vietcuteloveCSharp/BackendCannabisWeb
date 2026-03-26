namespace DAL.Entities
{
    public class Breeder :BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string BreederName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
		public string? Description { get; set; }
        public string? Website {  get; set; }
        public bool IsActive { get; set; }=true;
        public string Email {  get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;

		//navigation
		public virtual ICollection<Seed> Seeds { get; set; } = new HashSet<Seed>();
    }
}
