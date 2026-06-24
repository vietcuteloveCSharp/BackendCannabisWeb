using DAL.Entities.Inherited;

namespace DAL.Entities.Payment
{
	public class PaymentMethod:BaseEntity
	{
	
		public int Id { get; set; }

		public string Name { get; set; } = default!; // Ví dụ: CreditCard, PayPal, COD

		public string? Description { get; set; }

		// Navigation
		public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
	}
}
