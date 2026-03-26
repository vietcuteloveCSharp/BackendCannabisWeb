namespace DAL.Entities
{
    
    public class Address :BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Country { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string District { get; set; }= string.Empty;
        public string Commune { get; set; }= string.Empty;
        public string Road_Village_Hamlet { get; set; } = string.Empty;
        public string HouseNumber { get; set; }= string.Empty;
        public string PostalCode { get; set; }= string.Empty;
		public bool IsDefault {  get; set; } =false;
        //navigation
        public virtual User User { get; set; } = default!;
       
	}
}
