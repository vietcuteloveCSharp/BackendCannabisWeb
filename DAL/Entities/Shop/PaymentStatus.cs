namespace DAL.Entities.Shop
{
	public class PaymentStatus : BaseEntity
	{
		public int Id { get; set; }

		public string Name { get; set; } = default!; // Ví dụ: Pending, Paid, Failed

		public string? Description { get; set; }

		// Navigation
		public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
	}
}
