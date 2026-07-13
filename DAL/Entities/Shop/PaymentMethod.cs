using Shared.Common.Inherited;

namespace DAL.Entities.Shop
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
