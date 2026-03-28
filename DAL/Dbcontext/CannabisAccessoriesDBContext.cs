using DAl.Data;
using System.Linq.Expressions;


namespace DAL.Dbcontext
{
	public class CannabisAccessoriesDBContext : DbContext
	{
		public CannabisAccessoriesDBContext(DbContextOptions<CannabisAccessoriesDBContext> options) : base(options)
		{

		}
		public virtual DbSet<Address> Addresses { get; set; }
		public virtual DbSet<Brand> Brands { get; set; }
		public virtual DbSet<Breeder> Breeders { get; set; }
		public virtual DbSet<CarbonFilter> CarbonFilters { get; set; }
		public virtual DbSet<Cart> Carts { get; set; }
		public virtual DbSet<CartDetails> CartDetails { get; set; }
		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<ChipModel> ChipModels { get; set; }
		public virtual DbSet<Classification> Classifications { get; set; }
		public virtual DbSet<CoolingSystem> CoolingSystems { get; set; }
		public virtual DbSet<Dehumidifier> Dehumidifiers { get; set; }
		public virtual DbSet<GrowTent> GrowTents { get; set; }
		public virtual DbSet<GrowLight> GrowLights { get; set; }
		public virtual DbSet<AuditLog> AuditLogs { get; set; }
		public virtual DbSet<Nutrient> Nutrients { get; set; }
		public virtual DbSet<NutrientType> NutrientTypes { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<OrderItem> OrderItems { get; set; }
		public virtual DbSet<Payment> Payments { get; set; }
		public virtual DbSet<PowerSupply> PowerSupplies { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<ProductImage> ProductImages { get; set; }
		public virtual DbSet<Promotion> Promotions { get; set; }
		public virtual DbSet<PromotionCategory> PromotionCategories { get; set; }
		public virtual DbSet<PromotionProduct> PromotionProducts { get; set; }
		public virtual DbSet<Review> Reviews { get; set; }
		public virtual DbSet<Role> Roles { get; set; }
		public virtual DbSet<Seed> Seeds { get; set; }
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<ShippingMethod> ShippingMethods { get; set; }
		public virtual DbSet<Spectrum> Spectrums { get; set; }
		public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			//Đăng ký TẤT CẢ các class thực thi IEntityTypeConfiguration trong cùng Assembly
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CannabisAccessoriesDBContext).Assembly);

			// Apply base entity mapping cho tất cả entity kế thừa BaseEntity
			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
				{
					var method = typeof(ModelBuilder)
					.GetMethods()
					.First(m => m.Name == "Entity" && m.IsGenericMethod)
					.MakeGenericMethod(entityType.ClrType);

					var entityBuilder = method.Invoke(modelBuilder, null);

					var param = Expression.Parameter(entityType.ClrType, "e");
					var prop = Expression.Property(param, "IsDeleted");
					var body = Expression.Equal(prop, Expression.Constant(false));
					var lambda = Expression.Lambda(body, param);

					entityType.SetQueryFilter(lambda);
				}
			}

			DbInitializer.Seed(modelBuilder);

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
						entry.Entity.IsDeleted = false;
						break;

					case EntityState.Modified:
						entry.Entity.UpdatedAt = DateTime.UtcNow;
						break;

					case EntityState.Deleted:
						entry.State = EntityState.Modified;
						entry.Entity.IsDeleted = true;
						entry.Entity.DeletedAt = DateTime.UtcNow;
						break;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}
	}
}