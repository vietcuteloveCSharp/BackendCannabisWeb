namespace DAL.Repository.Implementations
{
	public class PromotionCategoryRepository : IPromotionCategoryRepository
	{
		private readonly CannabisAccessoriesDBContext _context;
		public PromotionCategoryRepository(CannabisAccessoriesDBContext context)
		{
			_context = context;
		}
		public async Task<PromotionCategory?> AddCategoryToPromotionAsync(PromotionCategory promotionCategory)
		{

			var exists = await _context.PromotionCategories
				.AnyAsync(x => x.PromotionId == promotionCategory.PromotionId && x.CategoryId == promotionCategory.CategoryId);
			if (exists) return null;
			_context.PromotionCategories.Add(promotionCategory);
			await _context.SaveChangesAsync();
			return promotionCategory;
		}

		public async Task<List<int>> GetCategoryIdsByPromotionAsync(int promotionId)
		{
			return await _context.PromotionCategories
			.Where(x => x.PromotionId == promotionId)
			.Select(x => x.CategoryId)
			.ToListAsync();
		}

		public async Task<bool> RemoveCategoryFromPromotionAsync(int promotionId, int categoryId)
		{
			var entity = await _context.PromotionCategories
		   .FirstOrDefaultAsync(x => x.PromotionId == promotionId && x.CategoryId == categoryId);

			if (entity == null)
				return false;

			_context.PromotionCategories.Remove(entity);
			await _context.SaveChangesAsync();

			return true;
		}

		public async Task SyncCategoriesForPromotionAsync(int promotionId, List<int> categoryIds)
		{
			// 1.Xoá tất cả sản phẩm cũ
			var existing = await _context.PromotionCategories
				.Where(x => x.PromotionId == promotionId)
				.ToListAsync();

			_context.PromotionCategories.RemoveRange(existing);

			// 2. Thêm danh sách mới
			var newEntities = categoryIds.Select(cid => new PromotionCategory
			{
				PromotionId = promotionId,
				CategoryId = cid
			});

			_context.PromotionCategories.AddRange(newEntities);

			// 3. Lưu thay đổi
			await _context.SaveChangesAsync();
		}
	}
}
