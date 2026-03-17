namespace DAL.Entities
{
    public class Payment :BaseEntity
    {
        [Key]
        public int PaymentId {  get; set; }
        [Required(ErrorMessage = "Id order  is required.")]
        public int OrderId {  get; set; }

        [StringLength(100,ErrorMessage = "Payment name no more than 100 characters.")]
        [Required(ErrorMessage = "Payment name is required.")]
        public string PaymentName { get; set; } = string.Empty; // Tên phương thức thanh toán
		public string Description { get; set; } = string.Empty;
        //navigation

        public virtual Order Order { get; set; } = default!; 

    }
}
