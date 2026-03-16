namespace DAL.Entities
{
    [Table("NutrientTypes",Schema = "Inventory")]   
    public class NutrientType :BaseEntity
    {
        [Key]
        public int NutrientTypeId { get; set; }
        [Required(ErrorMessage = "Nutrient name is required.")]
        [StringLength(150,ErrorMessage = "Nutrient name no more than 150 characters.")]
        public string NutrientName { get; set; } = string.Empty; // Name of the nutrient type
		public string? Description { get; set; }
        //navigation 
		public virtual ICollection<Nutrient> Nutrients { get; set; } = new HashSet<Nutrient>();
	}
}
