namespace DAL.Entities
{
    [Table("PowerSupplies", Schema = "lighting")]
    public class PowerSupply : BaseEntity
    {
        [Key]
        public int PowerSupplyId { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public EPowerSypplyType Type { get; set; } 
        public int Voltage { get; set; }
		public virtual ICollection<GrowLight> GrowLights { get; set; } = new List<GrowLight>();
    }
}
