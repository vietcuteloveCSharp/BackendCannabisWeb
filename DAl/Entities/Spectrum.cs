namespace DAL.Entities
{
    [Table("Spectrums",Schema = "Inventory")]   
    public class Spectrum : BaseEntity
    {
        [Key]
        public int SpectrumId { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public ESpectrumType Type { get; set; } 
        public string? Description { get; set; }
		public virtual ICollection<GrowLight> GrowLights { get; set; } = new List<GrowLight>();
	}
}
