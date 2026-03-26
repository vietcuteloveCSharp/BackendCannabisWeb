namespace DAL.Entities
{
    public class Payment :BaseEntity
    {
        [Key]
        public int Id {  get; set; }
        public int OrderId {  get; set; }

        public string PaymentName { get; set; } = string.Empty; // Tên phương thức thanh toán
		public string Description { get; set; } = string.Empty;
        //navigation

        public virtual Order Order { get; set; } = default!; 

    }
}
