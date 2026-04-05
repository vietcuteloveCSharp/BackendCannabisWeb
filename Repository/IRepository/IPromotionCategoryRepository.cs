using DAL.Entities.Promotion;

namespace Repository.IRepository
{
	public interface IPromotionCategoryRepository
	{
		Task<PromotionCategory?> AddCategoryToPromotionAsync(PromotionCategory promotionCategory);
		Task<bool> RemoveCategoryFromPromotionAsync(int promotionId, int categoryId);
		Task<List<int>> GetCategoryIdsByPromotionAsync(int promotionId);
		Task SyncCategoriesForPromotionAsync(int promotionId, List<int> categoryIds);
	}
}
