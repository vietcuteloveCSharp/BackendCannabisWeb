namespace DAL.Repository.Interfaces
{
	public interface IPromotionProductRepository
	{
		Task<PromotionProduct?> AddProductToPromotionAsync(PromotionProduct promotionProduct);
		Task<bool> RemoveProductFromPromotionAsync(int promotionId, int productId);
		Task<List<int>> GetProductIdsByPromotionAsync(int promotionId);
		Task SyncProductsForPromotionAsync(int promotionId, List<int> productIds);
	}
}
