//mapping nhiều ↔ nhiều giữa ProductVariant và AttributeValue
namespace DAL.Entities.Product
{
	public class ProductVariantAttribute
	{
		public int ProductVariantId { get; set; } // FK ProductVariant
		public int AttributeValueId { get; set; } // FK AttributeValue

		// Navigation
		public ProductVariant ProductVariant { get; set; } = default!;
		public AttributeValue AttributeValue { get; set; } = default!;
	}
}
