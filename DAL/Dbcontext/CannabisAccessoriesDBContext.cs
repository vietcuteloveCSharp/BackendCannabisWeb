namespace DAL.Dbcontext
{
	public class CannabisAccessoriesDBContext : DbContext
	{
		private readonly IAuditQueue _auditQueue;
		public CannabisAccessoriesDBContext(DbContextOptions<CannabisAccessoriesDBContext> options) : base(options)
		{

		}
		public virtual DbSet<Address> Addresses { get; set; }
		public virtual DbSet<Brand> Brands { get; set; }
		public virtual DbSet<Cart> Carts { get; set; }
		public virtual DbSet<CartItem> CartItems { get; set; }
		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<OrderItem> OrderItems { get; set; }
		public virtual DbSet<Payment> Payments { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<ProductImage> ProductImages { get; set; }
		public virtual DbSet<Promotion> Promotions { get; set; }
		public virtual DbSet<PromotionCategory> PromotionCategories { get; set; }
		public virtual DbSet<PromotionProduct> PromotionProducts { get; set; }
		public virtual DbSet<Review> Reviews { get; set; }
		public virtual DbSet<Role> Roles { get; set; }
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<ShippingMethod> ShippingMethods { get; set; }
		public virtual DbSet<UserRefreshToken> RefreshTokens { get; set; }
		public virtual DbSet<Inventory> Inventories { get; set; }
		public virtual DbSet<StockMovement> StockMovement { get; set; }
		public virtual DbSet<OrderHistory> OrderHistory { get; set; }
		public virtual DbSet<OrderStatus> OrderStatus { get; set; }
		public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
		public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }
		public virtual DbSet<AttributeValue> AttributeValues { get; set; }
		public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
		public virtual DbSet<ProductVariant> ProductVariants { get; set; }
		public virtual DbSet<ProductVariantAttribute> ProductVariantAttributes { get; set; }
		public virtual DbSet<ProductTag> ProductTags { get; set; }
		public virtual DbSet<Tag> Tags { get; set; }
		public virtual DbSet<Coupon> Coupons { get; set; }
		public virtual DbSet<CouponUsage> CouponUsages { get; set; }
		public virtual DbSet<Shipment> Shipments { get; set; }
		public virtual DbSet<ShipmentItem> ShipmentItems { get; set; }
		public virtual DbSet<ShipmentStatus> ShipmentStatuses { get; set; }
		public virtual DbSet<UserStatus> UserStatuses { get; set; }
		public virtual DbSet<Wishlist> Wishlists { get; set; }
		public virtual DbSet<UserSession> UserSessions { get; set; }



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			//Đăng ký TẤT CẢ các class thực thi IEntityTypeConfiguration trong cùng Assembly
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CannabisAccessoriesDBContext).Assembly);

			// Apply base entity mapping cho tất cả entity kế thừa BaseEntity
			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
				{
					var method = typeof(ModelBuilder)
					.GetMethods()
					.First(m => m.Name == "Entity" && m.IsGenericMethod)
					.MakeGenericMethod(entityType.ClrType);

					var entityBuilder = method.Invoke(modelBuilder, null);

					var param = Expression.Parameter(entityType.ClrType, "e");
					var prop = Expression.Property(param, nameof(ISoftDelete.IsDeleted));
					var body = Expression.Equal(prop, Expression.Constant(false));
					var lambda = Expression.Lambda(body, param);

					entityType.SetQueryFilter(lambda);
				}
			}


		}


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{

		}

		// lưu theo entity state 
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var entries = ChangeTracker.Entries<BaseEntity>();

			foreach (var entry in entries)
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedAt = DateTime.UtcNow;
						break;

					case EntityState.Modified:
						entry.Entity.UpdatedAt = DateTime.UtcNow;
						break;

					case EntityState.Deleted:
						if (entry.Entity is ISoftDelete softDelete)
						{
							// Soft delete
							entry.State = EntityState.Modified;
							softDelete.IsDeleted = true;
							softDelete.DeletedAt = DateTime.UtcNow;						
						}
						else
						{
							// Hard delete nếu không implement ISoftDelete
							entry.State = EntityState.Deleted;
						}
						break;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}
	}
}