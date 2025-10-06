namespace DAL.Entities
{
    [Table("ShippingMethod",Schema ="Oders")]
    public class ShippingMethod :BaseEntity
    {
        [Key]
        public int ShippingId {  get; set; }
        [Required(ErrorMessage ="Id Order is required.")]
        public int OrderId {  get; set; }
        [Required(ErrorMessage ="Name is required.")]
        [StringLength(150, ErrorMessage = "Name no more than 150 characters.")]
        public string Name { get; set; }  =string.Empty;
		[Required(ErrorMessage = "Carrier is required.")]
        [StringLength(150, ErrorMessage = "Carrierno more than 150 characters.")]
        public string Carrier { get; set; }  =string.Empty;
		public int EstimatedDeliveryDays { get; set; } // số ngày dự kiến
		public DateTime EstimatedDeliveryDate { get; set; }//  giao thực tế tính toán được
		//navigation
		public virtual Order? Order { get; set; }
    }
}
