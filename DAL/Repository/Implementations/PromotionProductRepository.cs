namespace DAL.Repository.Implementations
{
	public class PromotionProductRepository : IPromotionProductRepository
	{
		private readonly CannabisAccessoriesDBContext _context;
		public PromotionProductRepository(CannabisAccessoriesDBContext context)
		{
			_context = context;
		}
		// add product to promotion
		public async Task<PromotionProduct?> AddProductToPromotionAsync(PromotionProduct promotionProduct)
		{
			var exists = await _context.PromotionProducts
				.AnyAsync(x => x.PromotionId == promotionProduct.PromotionId && x.ProductId == promotionProduct.ProductId);
			if (exists) return null;
			_context.PromotionProducts.Add(promotionProduct);
			await _context.SaveChangesAsync();
			return promotionProduct;

		}
		// get product ids by promotion id
		public async Task<List<int>> GetProductIdsByPromotionAsync(int promotionId)
		{
			return await _context.PromotionProducts
			.Where(x => x.PromotionId == promotionId)
			.Select(x => x.ProductId)
			.ToListAsync();
		}
		// remove product from promotion
		public async Task<bool> RemoveProductFromPromotionAsync(int promotionId, int productId)
		{
		  var entity = await _context.PromotionProducts
			.FirstOrDefaultAsync(x => x.PromotionId == promotionId && x.ProductId == productId);

			if (entity == null)
				return false;

			_context.PromotionProducts.Remove(entity);
			await _context.SaveChangesAsync();

			return true;
		}
		// Synchronize products for a promotion
		public async Task SyncProductsForPromotionAsync(int promotionId, List<int> productIds)
		{
			// 1. Xoá tất cả sản phẩm cũ
			var existing = await _context.PromotionProducts
				.Where(x => x.PromotionId == promotionId)
				.ToListAsync();

			_context.PromotionProducts.RemoveRange(existing);

			// 2. Thêm danh sách mới
			var newEntities = productIds.Select(pid => new PromotionProduct
			{
				PromotionId = promotionId,
				ProductId = pid
			});

			_context.PromotionProducts.AddRange(newEntities);

			// 3. Lưu thay đổi
			await _context.SaveChangesAsync();
		}
	}
}
