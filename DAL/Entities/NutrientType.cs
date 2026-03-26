namespace DAL.Entities
{
    public class NutrientType :BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string NutrientName { get; set; } = string.Empty; // Name of the nutrient type
		public string? Description { get; set; }
        //navigation 
		public virtual ICollection<Nutrient> Nutrients { get; set; } = new HashSet<Nutrient>();
	}
}
