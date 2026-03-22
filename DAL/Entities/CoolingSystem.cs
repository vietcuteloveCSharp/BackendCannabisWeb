namespace DAL.Entities
{
    public class CoolingSystem : BaseEntity
    {
        [Key]
        public int CoolingSystemId { get; set; }
        [Required(ErrorMessage ="Type is required.")]
        [Column(TypeName = "nvarchar(20)")]
        public ECoolingType Type { get; set; } 
        public string? Description { get; set; }
		public virtual ICollection<GrowLight> GrowLights { get; set; } = new HashSet<GrowLight>();
    }
}
