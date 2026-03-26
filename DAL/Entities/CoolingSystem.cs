namespace DAL.Entities
{
    public class CoolingSystem : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public ECoolingType Type { get; set; } 
        public string? Description { get; set; }
		public virtual ICollection<GrowLight> GrowLights { get; set; } = new HashSet<GrowLight>();
    }
}
