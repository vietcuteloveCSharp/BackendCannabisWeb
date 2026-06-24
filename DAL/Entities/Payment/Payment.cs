
namespace DAL.Entities.Payment
{
	public class Payment : BaseEntity, ISoftDelete
	{
		public int Id { get; set; }

		public int OrderId { get; set; } // FK Order

		public int PaymentMethodId { get; set; } // FK PaymentMethod

		public int PaymentStatusId { get; set; } // FK PaymentStatus

		public decimal Amount { get; set; }

		public string? TransactionId { get; set; } // ID từ cổng thanh toán

		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }

		// Navigation
		public Order.Order Order { get; set; } = default!;
		public PaymentMethod PaymentMethod { get; set; } = default!;
		public PaymentStatus PaymentStatus { get; set; } = default!;
	}
}
