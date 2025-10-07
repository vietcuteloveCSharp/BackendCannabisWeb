namespace DAL.Dbcontext
{
	public class CannabisAccessorriesDBContext : DbContext
	{


		public CannabisAccessorriesDBContext(DbContextOptions<CannabisAccessorriesDBContext> options) : base(options)
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
			// Apply base entity mapping cho tất cả entity kế thừa BaseEntity
			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
				{
					modelBuilder.Entity(entityType.ClrType).Property<DateTime>("CreatedAt")
						.HasDefaultValueSql("GETUTCDATE()")
						.ValueGeneratedOnAdd();

					modelBuilder.Entity(entityType.ClrType).Property<DateTime?>("UpdatedAt");

					modelBuilder.Entity(entityType.ClrType).Property<bool>("IsDeleted")
						.HasDefaultValue(false);

					modelBuilder.Entity(entityType.ClrType).Property<DateTime?>("DeletedAt");
				}
			}
			#region Table Addresses
			modelBuilder.Entity<Address>(entity =>
			{
				entity.ToTable("Addresses", "Users");

				entity.HasKey(a => a.AddressId);
				entity.Property(a => a.AddressId)
					  .ValueGeneratedOnAdd();

				entity.Property(a => a.UserId).IsRequired();
				entity.HasOne(a => a.User)
					  .WithMany(u => u.Addresses)
					  .HasForeignKey(a => a.UserId)
					  .OnDelete(DeleteBehavior.Cascade)
					  .HasConstraintName("FK_ADDRESS_USER_USERID");

				entity.Property(a => a.Country).HasMaxLength(150).IsRequired();
				entity.Property(a => a.Province).HasMaxLength(150).IsRequired();
				entity.Property(a => a.District).HasMaxLength(150).IsRequired();
				entity.Property(a => a.Commune).HasMaxLength(150).IsRequired();
				entity.Property(a => a.Road_Village_Hamlet).HasMaxLength(150).IsRequired();
				entity.Property(a => a.HouseNumber).HasMaxLength(20).IsRequired();
				entity.Property(a => a.PostalCode).HasMaxLength(30).IsRequired();
				entity.Property(a => a.IsDefault).HasDefaultValue(false);
			});
			#endregion
			#region Table Brands
			modelBuilder.Entity<Brand>(entity =>
			{
				entity.ToTable("Brands", "Products");
				entity.HasKey(b => b.BrandId);

				entity.Property(b => b.BrandName)
					  .HasMaxLength(255)
					  .IsRequired();

				entity.Property(b => b.Country)
					  .HasMaxLength(150);

				entity.Property(b => b.Description)
					  .HasMaxLength(1000);

				entity.Property(b => b.Website)
					  .HasMaxLength(255);

				entity.Property(b => b.IsActive)
					  .HasDefaultValue(true);

				// ✅ Index: BrandName (search nhiều) 
				entity.HasIndex(b => b.BrandName)
					  .IsUnique()
					  .HasDatabaseName("IX_Brands_BrandName");

			});
			#endregion
			#region Table Breeder
			modelBuilder.Entity<Breeder>(entity =>
			{
				entity.ToTable("Breeders", "Products");
				entity.HasKey(b => b.BreederId);

				entity.Property(b => b.BreederName)
					  .HasMaxLength(255)
					  .IsRequired();

				entity.Property(b => b.Country)
					  .HasMaxLength(150);

				entity.Property(b => b.Description)
					  .HasMaxLength(1000);

				entity.Property(b => b.Website)
					  .HasMaxLength(255);

				entity.Property(b => b.IsActive)
					  .HasDefaultValue(true);

				entity.Property(b => b.Email)
					  .HasMaxLength(150)
					  .IsRequired();

				entity.HasIndex(b => b.Email)
					  .IsUnique()
					  .HasDatabaseName("IX_Breeder_Email");

				entity.Property(b => b.PhoneNumber)
					  .HasMaxLength(20);
			});
			#endregion
			#region Table ChipModels
			modelBuilder.Entity<ChipModel>(entity =>
			{
				entity.ToTable("ChipModels", "Inventory");
				entity.HasKey(c => c.ChipModelId);

				entity.Property(c => c.Manufacturer)
					  .HasMaxLength(100)
					  .IsRequired();

				entity.Property(c => c.ModelChip)
					  .HasMaxLength(100)
					  .IsRequired();

				entity.Property(c => c.Generation)
					  .HasMaxLength(50);

				entity.Property(c => c.Efficiency)
					  .HasColumnType("decimal(5,2)")
					  .IsRequired();

				entity.Property(c => c.Description)
					  .HasMaxLength(1000);

				// ✅ Index hữu ích khi search/filter

				entity.HasIndex(c => c.ModelChip)
					  .HasDatabaseName("IX_ChipModels_ModelChip");
			});

			#endregion
			#region Table CarbonFilters
			modelBuilder.Entity<CarbonFilter>(entity =>
			{
				entity.ToTable("CarbonFilters", "Inventory");
				entity.HasKey(cf => cf.CarbonFilterId);
				entity.Property(cf => cf.AirflowRate).HasMaxLength(150);
				entity.Property(cf => cf.Price).HasColumnType("decimal(10,2)");
				entity.Property(cf => cf.Description).HasMaxLength(1000);
				entity.Property(cf => cf.BrandId).IsRequired();
				entity.HasOne(cf => cf.Brand)
					  .WithMany(b => b.CarbonFilters)
					  .HasForeignKey(cf => cf.BrandId)
					  .OnDelete(DeleteBehavior.Restrict)
					  .HasConstraintName("FK_CARBONFILTER_BRAND_BRANDID");

				entity.HasOne(cf => cf.Product)
					  .WithOne(p => p.CarbonFilter)
					  .HasForeignKey<CarbonFilter>(cf => cf.CarbonFilterId)
					  .OnDelete(DeleteBehavior.Cascade);

				entity.HasIndex(cf => cf.BrandId)
					  .HasDatabaseName("IX_CarbonFilters_BrandId");
				entity.HasOne(d => d.Product)
					  .WithOne(p => p.CarbonFilter)
					  .HasForeignKey<CarbonFilter>(d => d.ProductId)
					  .HasConstraintName("FK_CARBONFILTER_PRODUCT_PRODUCTID")
					  .OnDelete(DeleteBehavior.Cascade)
					  .IsRequired();
				entity.HasIndex(g => g.ProductId)
					.HasDatabaseName("IX_CarbonFilter_ProductId");
			});
			#endregion
			#region Table Carts
			modelBuilder.Entity<Cart>(entity =>
			{
				entity.ToTable("Carts", "Orders");
				entity.HasKey(c => c.CartId);

				entity.Property(c => c.UserId)
					  .IsRequired();

				entity.Property(c => c.Session_Id)
					  .HasMaxLength(255)
					  .IsRequired();

				entity.Property(c => c.Price)
					  .HasColumnType("decimal(10,2)")
					  .IsRequired();

				entity.Property(c => c.Status)
					  .HasConversion<string>()
					  .HasMaxLength(20)
					  .IsRequired();

				entity.ToTable("Carts", "Orders", t =>
				{
					t.HasCheckConstraint("CK_Carts_UserOrSession",
						"(UserId IS NOT NULL AND Session_Id IS NULL) OR (UserId IS NULL AND Session_Id IS NOT NULL)");
				});
				// ✅ Unique index: chỉ một giỏ hàng active / user
				entity.HasIndex(e => e.UserId)
					.HasDatabaseName("UX_Cart_User")
					.IsUnique()
					.HasFilter("[Status] = 'Active' AND [UserId] IS NOT NULL");

				// ✅ Unique index: chỉ một giỏ hàng active / session
				entity.HasIndex(e => e.Session_Id)
					.HasDatabaseName("UX_Cart_Session")
					.IsUnique()
					.HasFilter("[Status] = 'Active' AND [Session_Id] IS NOT NULL");

			});
			#endregion
			#region  Table CartDetails
			modelBuilder.Entity<CartDetails>(entity =>
			{
				entity.ToTable("CartDetails", "Orders");
				entity.HasKey(cd => cd.CartDetailsId);

				entity.Property(cd => cd.Price).HasColumnType("decimal(10,2)");
				entity.Property(cd => cd.Quantity).IsRequired();

				entity.Property(cd => cd.CartId).IsRequired();
				entity.HasOne(cd => cd.Cart)
					  .WithMany(c => c.CartDetails)
					  .HasForeignKey(cd => cd.CartId)
					  .HasConstraintName("FK_CARTDETAILS_CART_CARTID")
					  .OnDelete(DeleteBehavior.Cascade);

				entity.Property(cd => cd.ProductId).IsRequired();
				entity.HasOne(cd => cd.Product)
					  .WithMany(p => p.CartsDetails)
					  .HasForeignKey(cd => cd.ProductId)
					  .HasConstraintName("FK_CARTDETAILS_PRODUCT_PRODUCTID")
					  .OnDelete(DeleteBehavior.Restrict);

				entity.HasIndex(cd => cd.CartId)
					  .HasDatabaseName("IX_CartDetails_CartId");

				entity.HasIndex(cd => cd.ProductId)
					  .HasDatabaseName("IX_CartDetails_ProductId");
			});
			#endregion
			#region Table Categories
			modelBuilder.Entity<Category>(entity =>
			{
				entity.ToTable("Categories", "Products");
				entity.HasKey(c => c.CategoryId);

				entity.Property(c => c.CategoryName).HasMaxLength(100).IsRequired();

				entity.HasMany(c => c.Products)
					  .WithOne(p => p.Category)
					  .HasForeignKey(p => p.CategoryId)
					  .OnDelete(DeleteBehavior.Restrict)
					  .HasConstraintName("FK_PRODUCT_CATEGORY_CATEGORYID");

				entity.HasMany(c => c.PromotionCategories)
					  .WithOne(pc => pc.Category)
					  .HasForeignKey(pc => pc.CategoryId)
					  .OnDelete(DeleteBehavior.Cascade)
					  .HasConstraintName("FK_PROMOTIONCATEGORY_CATEGORY_CATEGORYID");

				entity.HasIndex(c => c.CategoryName)
					  .IsUnique()
					  .HasDatabaseName("IX_Categories_CategoryName");
			});
			#endregion
			#region Table Classification
			modelBuilder.Entity<Classification>(entity =>
			{
				entity.ToTable("Classifications", "Products");
				entity.HasKey(c => c.ClassificationId);

				entity.Property(c => c.ClassificationName)
					  .HasMaxLength(150)
					  .IsRequired();

				entity.Property(c => c.Quantity)
					  .IsRequired();

				entity.Property(c => c.Description)
					  .HasMaxLength(1000); // tránh nvarchar(max)

				entity.Property(c => c.IsActive)
					  .HasDefaultValue(true);

				// ✅ Index để tìm nhanh theo tên
				entity.HasIndex(c => c.ClassificationName)
					  .IsUnique()
					  .HasDatabaseName("UX_Classifications_ClassificationName");
			});
			#endregion
			#region Table CoolingSystems
			modelBuilder.Entity<CoolingSystem>(entity =>
			{
				entity.ToTable("CoolingSystems", "Inventory");
				entity.HasKey(c => c.CoolingSystemId);

				entity.Property(c => c.Type)
					  .HasConversion<string>()
					  .HasMaxLength(20)
					  .IsRequired();
				entity.Property(c => c.Description)
					  .HasMaxLength(1000);
			});
			#endregion
			#region Table Dehumidifiers
			modelBuilder.Entity<Dehumidifier>(entity =>
			{
				entity.ToTable("Dehumidifiers", "Inventory");
				entity.HasKey(d => d.DehumidifierId);
				entity.Property(d => d.DehumidificationCapacity).HasColumnType("decimal(3,2)");
				entity.Property(d => d.CoverageArea).HasColumnType("decimal(10,2)");
				entity.Property(d => d.NoiseLevel).HasColumnType("decimal(5,2)");
				entity.Property(d => d.PowerConsumption).HasColumnType("decimal(10,2)");
				entity.Property(d => d.Description).HasMaxLength(1000);
				entity.Property(d => d.BrandId).IsRequired();
				entity.HasOne(d => d.Brand)
					  .WithMany(b => b.Dehumidifiers)
					  .HasForeignKey(d => d.BrandId)
					  .HasConstraintName("FK_DEHUMIDIFIERS_BRAND_BRANDID")
					  .OnDelete(DeleteBehavior.Restrict);
				entity.HasOne(d => d.Product)
					  .WithOne(p => p.Dehumidifier)
					  .HasForeignKey<Dehumidifier>(d => d.ProductId)
					  .HasConstraintName("FK_DEHUMIDIFIERS_PRODUCT_PRODUCTID")
					  .OnDelete(DeleteBehavior.Cascade)
					  .IsRequired();
				entity.HasIndex(g => g.ProductId)
					.HasDatabaseName("IX_Dehumidifier_ProductId");

			});
			#endregion
			#region Table GrowTent
			modelBuilder.Entity<GrowTent>(entity =>
			{
				entity.ToTable("GrowTents", "Products");
				entity.HasKey(gt => gt.GrowtentId);

				entity.Property(gt => gt.BrandId)
					  .IsRequired();

				entity.Property(gt => gt.Dimensions)
					  .HasMaxLength(100)
					  .IsRequired();

				entity.Property(gt => gt.Material)
					  .HasMaxLength(255)
					  .IsRequired();

				entity.Property(gt => gt.Waterproof)
					  .HasDefaultValue(false);

				entity.Property(gt => gt.Quantity)
					  .IsRequired();

				entity.Property(gt => gt.Price)
					  .HasColumnType("decimal(10,2)")
					  .IsRequired();

				entity.Property(gt => gt.FrameMaterial)
					  .HasMaxLength(255)
					  .IsRequired();

				entity.Property(gt => gt.WarrantyPeriod)
					  .IsRequired();

				entity.Property(gt => gt.Description)
					  .HasMaxLength(1000);

				entity.HasOne(gt => gt.Brand)
					  .WithMany(b => b.GrowTents)
					  .HasForeignKey(gt => gt.BrandId)
					  .OnDelete(DeleteBehavior.Restrict)
					  .HasConstraintName("FK_GROWTENT_BRAND_BRANDID");
				entity.HasIndex(gt => gt.BrandId)
					  .HasDatabaseName("IX_GrowTents_BrandId");
				entity.HasOne(d => d.Product)
					  .WithOne(p => p.GrowTent)
					  .HasForeignKey<GrowTent>(d => d.ProductId)
					  .HasConstraintName("FK_GROWTENT_PRODUCT_PRODUCTID")
					  .OnDelete(DeleteBehavior.Cascade)
					  .IsRequired();
				entity.HasIndex(g => g.ProductId)
					.HasDatabaseName("IX_Growtent_ProductId");
			});
			#endregion
			#region Table GrowLights
			modelBuilder.Entity<GrowLight>(entity =>
			{
				entity.ToTable("GrowLights", "Inventory");
				entity.HasKey(gl => gl.GrowLightId);

				entity.Property(gl => gl.BrandId)
					  .IsRequired();

				entity.Property(gl => gl.Quantity)
					  .IsRequired();

				entity.Property(gl => gl.Wattage)
					  .IsRequired();

				entity.Property(gl => gl.Price)
					  .HasColumnType("decimal(10,2)")
					  .IsRequired();

				entity.Property(gl => gl.CoverageArea)
					  .IsRequired();

				entity.Property(gl => gl.WarrantyPeriod)
					  .IsRequired();

				entity.Property(gl => gl.PowerSupplyId)
					  .IsRequired();

				entity.Property(gl => gl.ChipModelId)
					  .IsRequired();

				entity.Property(gl => gl.CoolingSystemId)
					  .IsRequired();

				entity.Property(gl => gl.SpectrumId)
					  .IsRequired();

				entity.Property(gl => gl.Lifespan)
					  .IsRequired();

				entity.Property(gl => gl.ModelNumber)
					  .HasMaxLength(100)
					  .IsRequired();

				entity.Property(gl => gl.Description)
					  .HasMaxLength(1000);


				entity.HasOne(gl => gl.Brand)
					  .WithMany(b => b.GrowLights)
					  .HasForeignKey(gl => gl.BrandId)
					  .OnDelete(DeleteBehavior.Restrict)
					  .HasConstraintName("FK_GROWLIGHT_BRAND");

				entity.HasOne(gl => gl.PowerSupply)
					  .WithMany(ps => ps.GrowLights)
					  .HasForeignKey(gl => gl.PowerSupplyId)
					  .OnDelete(DeleteBehavior.Restrict)
					  .HasConstraintName("FK_GROWLIGHT_POWERSUPPLY");

				entity.HasOne(gl => gl.ChipModel)
					  .WithMany(cm => cm.GrowLights)
					  .HasForeignKey(gl => gl.ChipModelId)
					  .OnDelete(DeleteBehavior.Restrict)
					  .HasConstraintName("FK_GROWLIGHT_CHIPMODEL");

				entity.HasOne(gl => gl.CoolingSystem)
					  .WithMany(cs => cs.GrowLights)
					  .HasForeignKey(gl => gl.CoolingSystemId)
					  .OnDelete(DeleteBehavior.Restrict)
					  .HasConstraintName("FK_GROWLIGHT_COOLINGSYSTEM");

				entity.HasOne(gl => gl.Spectrum)
					  .WithMany(s => s.GrowLights)
					  .HasForeignKey(gl => gl.SpectrumId)
					  .OnDelete(DeleteBehavior.Restrict)
					  .HasConstraintName("FK_GROWLIGHT_SPECTRUM");


				entity.HasIndex(gl => gl.BrandId)
					  .HasDatabaseName("IX_GrowLights_BrandId");

				entity.HasIndex(gl => gl.ChipModelId)
					  .HasDatabaseName("IX_GrowLights_ChipModelId");

				entity.HasIndex(gl => gl.PowerSupplyId)
					  .HasDatabaseName("IX_GrowLights_PowerSupplyId");

				entity.HasIndex(gl => gl.CoolingSystemId)
					  .HasDatabaseName("IX_GrowLights_CoolingSystemId");

				entity.HasIndex(gl => gl.SpectrumId)
					  .HasDatabaseName("IX_GrowLights_SpectrumId");
				entity.HasOne(d => d.Product)
					  .WithOne(p => p.GrowLight)
					  .HasForeignKey<GrowLight>(d => d.ProductId)
					  .HasConstraintName("FK_GROWLIGHT_PRODUCT_PRODUCTID")
					  .OnDelete(DeleteBehavior.Cascade)
					  .IsRequired();
				entity.HasIndex(g => g.ProductId)
					.HasDatabaseName("IX_Growlight_ProductId");
			});
			#endregion
			#region Table AuditLogs
			modelBuilder.Entity<AuditLog>(entity =>
			{
				entity.ToTable("AuditLogs", "Logs");

				entity.HasKey(e => e.AuditLogId);

				entity.Property(e => e.TableName)
					  .HasMaxLength(150)
					  .IsRequired();

				entity.Property(e => e.RecordId)
					  .HasMaxLength(100)
					  .IsRequired();

				entity.Property(e => e.Action)
					  .HasConversion<string>()   // enum -> string
					  .HasMaxLength(20)
					  .IsRequired();

				entity.Property(e => e.ColumnName)
					  .HasMaxLength(150);

				entity.Property(e => e.OldValue)
					  .HasColumnType("nvarchar(max)");
				entity.Property(e => e.NewValue)
					  .HasColumnType("nvarchar(max)");

				entity.Property(e => e.Description)
					 .HasMaxLength(1000);

				entity.Property(e => e.CreatedAt)
					  .HasDefaultValueSql("GETUTCDATE()"); // default từ SQL server

				// nếu muốn join sang Users
				entity.HasOne(e => e.User)
					  .WithMany(e=>e.AuditLogs)
					  .HasForeignKey(e => e.UserId)
					  .OnDelete(DeleteBehavior.SetNull);

				entity.HasOne(e => e.Role)
					.WithMany(r => r.AuditLogs)
					.HasForeignKey(e => e.RoleId)
					 .OnDelete(DeleteBehavior.SetNull)
					.HasConstraintName("FK_AuditLog_Role_RoleId");
				entity.Property(e => e.RoleName).IsRequired().HasMaxLength(100);
				entity.HasIndex(e => e.TableName);
				entity.HasIndex(e => e.Action);
				entity.HasIndex(e => e.CreatedAt);
			});
			#endregion
			#region Table Nutrients
			modelBuilder.Entity<Nutrient>()
				.HasKey(c => c.NutrientId);
			modelBuilder.Entity<Nutrient>()
				.Property(c => c.NutrientId)
				.ValueGeneratedOnAdd();
			modelBuilder.Entity<Nutrient>()
				.HasOne(c => c.Brand)
				.WithMany(c => c.Nutrients)
				.HasForeignKey(c => c.BrandId)
				.HasConstraintName("FK_NUTRIENT_BRAND_BRANDID")
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);
			modelBuilder.Entity<Nutrient>()
				.HasOne(c => c.NutrientType)
				.WithMany(c => c.Nutrients)
				.HasForeignKey(c => c.NutrientTypeId)
				.HasConstraintName("FK_NUTRIENT_NUTRIENTTYPE_NUTRIENTTYPEID")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			modelBuilder.Entity<Nutrient>()
				.Property<int>(c => c.Quantity)
				.IsRequired();
			modelBuilder.Entity<Nutrient>()
				.Property(c => c.Price)
				.HasPrecision(10, 2)
				.IsRequired();
			modelBuilder.Entity<Nutrient>()
				.Property<int>(c => c.VolumeMl)
				.IsRequired();
			modelBuilder.Entity<Nutrient>()
				.Property(c => c.Ingredients)
				.HasMaxLength(255);
			modelBuilder.Entity<Nutrient>()
				.Property(c => c.NpkRatio)
				.HasMaxLength(50);
			modelBuilder.Entity<Nutrient>()
				.Property<bool>(c => c.IsOrganic)
				.HasDefaultValue(false);
			modelBuilder.Entity<Nutrient>()
				.Property(c => c.Description)
				.HasMaxLength(1000);
			modelBuilder.Entity<Nutrient>()
				.Property(c => c.StorageInstructions)
				.HasMaxLength(255);
			modelBuilder.Entity<Nutrient>()
				.HasIndex(c => c.BrandId)
				.HasDatabaseName("IX_Nutrient_BrandId");
			modelBuilder.Entity<Nutrient>()
				.HasIndex(c => c.NutrientTypeId)
				.HasDatabaseName("IX_Nutrient_NutrientTypeId");
			modelBuilder.Entity<Nutrient>()
				.Property(c => c.ProductId).IsRequired();
			modelBuilder.Entity<Nutrient>()
				.HasOne(c => c.Product)
				.WithOne(c => c.Nutrient)
				.HasForeignKey<Nutrient>(c => c.ProductId)
				.HasConstraintName("FK_NUTRIENT_PRODUCT_PRODUCTID")
				.OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<Nutrient>().HasIndex(g => g.ProductId)
				.HasDatabaseName("IX_Nutrient_ProductId");
			#endregion
			#region Table NutrientTypes
			modelBuilder.Entity<NutrientType>()
				.HasKey(c => c.NutrientTypeId);
			modelBuilder.Entity<NutrientType>()
				.Property(c => c.NutrientTypeId)
				.ValueGeneratedOnAdd();
			modelBuilder.Entity<NutrientType>()
				.Property(c => c.NutrientName)
				.HasMaxLength(150)
				.IsRequired();
			modelBuilder.Entity<NutrientType>()
				.Property(c => c.Description)
				.HasMaxLength(1000);
			#endregion
			#region Table Orders
			modelBuilder.Entity<Order>()
				.HasKey(c => c.OrderId);
			modelBuilder.Entity<Order>()
				.Property(c => c.OrderId)
				.ValueGeneratedOnAdd();
			modelBuilder.Entity<Order>()
				.HasOne(c => c.Buyer)
				.WithMany(c => c.OrdersAsBuyer)
				.HasForeignKey(c => c.BuyerId)
				.HasConstraintName("FK_ORDER_BUYER_BUYERID")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			modelBuilder.Entity<Order>()
				.HasOne(c => c.Seller)
				.WithMany(c => c.OrdersAsSeller)
				.HasForeignKey(c => c.SellerId)
				.HasConstraintName("FK_ORDER_SELLER_SELLERID")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			//modelBuilder.Entity<Order>()
			//    .HasOne(c => c.ShippingMethod)
			//    .WithOne(c => c.Order)
			//    .HasForeignKey<ShippingMethod>(p => p.OrderId)
			//    .HasConstraintName("FK_ORDER_SHIPPINGMETHOD")
			//    .IsRequired();
			modelBuilder.Entity<Order>()
				.Property(c => c.OrderSatus)
				.HasConversion<string>()
				.IsRequired();
			modelBuilder.Entity<Order>()
				.Property(c => c.TotalAmount)
				.HasPrecision(10, 2)
				.IsRequired();
			modelBuilder.Entity<Order>()
				.Property(c => c.TrackingNumber)
				.HasMaxLength(50)
				.IsRequired();
			modelBuilder.Entity<Order>()
				.Property(c => c.ShippingFee)
				.HasPrecision(10, 2);
			modelBuilder.Entity<Order>()
				.Property(c => c.ShippingAddress)
				.HasMaxLength(2000);
			#endregion
			#region Table OrderItems
			modelBuilder.Entity<OrderItem>()
				.HasKey(c => c.OrderItemId);
			modelBuilder.Entity<OrderItem>()
				.Property(c => c.OrderItemId)
				.ValueGeneratedOnAdd();
			modelBuilder.Entity<OrderItem>()
				.HasOne(c => c.Oder)
				.WithMany(c => c.OrderItems)
				.HasForeignKey(c => c.OrderId)
				.HasConstraintName("FK_ODERITEM_ODER")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			modelBuilder.Entity<OrderItem>()
				.HasOne(c => c.Product)
				.WithMany(c => c.OderItems)
				.HasForeignKey(c => c.ProductId)
				.HasConstraintName("FK_ODERITEM_PRODUCT")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			modelBuilder.Entity<OrderItem>()
				.Property<int>(c => c.Quantity);
			modelBuilder.Entity<OrderItem>()
				.Property(c => c.Price)
				.HasPrecision(10, 2);
			#endregion
			#region Table Payments
			modelBuilder.Entity<Payment>()
				.HasKey(c => c.PaymentId);
			modelBuilder.Entity<Payment>()
				.Property(c => c.PaymentId)
				.ValueGeneratedOnAdd();
			modelBuilder.Entity<Payment>()
				.Property(c => c.PaymentName)
				.HasMaxLength(300)
				.IsRequired();
			modelBuilder.Entity<Payment>()
				.HasOne(c => c.Order)
				.WithOne(c => c.Payment)
				.HasForeignKey<Payment>(c => c.OrderId)
				.HasConstraintName("FK_PAYMENT_ORDER_ORDERID")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			modelBuilder.Entity<Payment>()
			   .Property(c => c.Description)
			   .HasMaxLength(1000);
			modelBuilder.Entity<Payment>()
				.HasIndex(c => c.OrderId)
				.HasDatabaseName("IX_Payment_OrderId");
			modelBuilder.Entity<Payment>()
				.HasIndex(c => c.PaymentName)
				.HasDatabaseName("IX_Payment_PaymentName");
			#endregion
			#region Table PowerSupplys
			modelBuilder.Entity<PowerSupply>()
				.HasKey(c => c.PowerSupplyId);
			modelBuilder.Entity<PowerSupply>()
				.Property(c => c.PowerSupplyId)
				.ValueGeneratedOnAdd();
			modelBuilder.Entity<PowerSupply>()
				.Property(c => c.Type)
				.HasConversion<string>()
				.IsRequired();
			modelBuilder.Entity<PowerSupply>()
				.Property<int>(c => c.Voltage)
				.IsRequired();
			#endregion
			#region Table Products
			modelBuilder.Entity<Product>()
				.HasKey(c => c.ProductId);
			modelBuilder.Entity<Product>()
				.Property(c => c.ProductId)
				.ValueGeneratedOnAdd();
			modelBuilder.Entity<Product>()
				.Property(c => c.ProductName)
				.HasMaxLength(255)
				.IsRequired();
			modelBuilder.Entity<Product>()
				.HasOne(c => c.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(c => c.CategoryId)
				.HasConstraintName("FK_PRODUCT_CATEGORY_CATEGORYID")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			modelBuilder.Entity<Product>()
			   .Property<bool>(c => c.IsActive)
			   .HasDefaultValue(true)
			   .IsRequired();
			modelBuilder.Entity<Product>()
				.HasIndex(c => c.ProductName)
				.HasDatabaseName("IX_Product_ProductName");

			#endregion
			#region Table ProductImages
			modelBuilder.Entity<ProductImage>()
				.HasKey(c => c.ProductImageId);
			modelBuilder.Entity<ProductImage>()
				.Property(c => c.ProductImageId)
				.ValueGeneratedOnAdd();
			modelBuilder.Entity<ProductImage>()
				.HasOne(c => c.Product)
				.WithMany(c => c.ProductImages)
				.HasForeignKey(c => c.ProductId)
				.HasConstraintName("FK_PRODUCTIMAGE_PRODUCT_PRODUCTID")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			modelBuilder.Entity<ProductImage>()
				.Property<string>(c => c.ImageUrl)
				.IsRequired();
			modelBuilder.Entity<ProductImage>()
				.Property(c => c.IsMainImage)
				.HasDefaultValue(false);

			#endregion
			#region Table Promotions
			modelBuilder.Entity<Promotion>()
				.HasKey(c => c.PromotionId);
			modelBuilder.Entity<Promotion>()
				.Property(c => c.PromotionId)
				.ValueGeneratedOnAdd();
			modelBuilder.Entity<Promotion>()
				.Property(c => c.PromotionName)
				.HasColumnType("NVARCHAR(255)")
				.IsRequired();
			modelBuilder.Entity<Promotion>()
				.Property(c => c.Description)
				.HasMaxLength(1000);
			modelBuilder.Entity<Promotion>()
				.Property(c => c.DiscountType)
				.HasConversion<string>()
				.IsRequired();
			modelBuilder.Entity<Promotion>()
				.Property(c => c.DiscountValue)
				.HasPrecision(12, 2)
				.IsRequired();
			modelBuilder.Entity<Promotion>()
				.Property(c => c.MinimumOrderValue)
				.HasPrecision(12, 2)
				.IsRequired();
			modelBuilder.Entity<Promotion>()
				.Property(c => c.MaximumDiscountValue)
				.HasPrecision(12, 2)
				.IsRequired();
			modelBuilder.Entity<Promotion>()
				.Property(c => c.MinimumQuantity)
				.IsRequired();
			modelBuilder.Entity<Promotion>()
			  .Property(a => a.StartDate)
			  .HasDefaultValueSql("CURRENT_TIMESTAMP")
			  .IsRequired();
			modelBuilder.Entity<Promotion>()
			  .Property(a => a.EndDate)
			  .HasDefaultValueSql("CURRENT_TIMESTAMP")
			  .IsRequired();
			modelBuilder.Entity<Promotion>()
			   .Property(a => a.IsActive)
			   .HasDefaultValue(true)
			   .IsRequired();
			modelBuilder.Entity<Promotion>()
				.HasIndex(c => c.PromotionName)
				.HasDatabaseName("IX_Promotion_PromotionName");

			#endregion
			#region Table PromotionCategories
			modelBuilder.Entity<PromotionCategory>()
				.HasKey(pc => new { pc.PromotionId, pc.CategoryId });
			modelBuilder.Entity<PromotionCategory>()
				.HasOne(c => c.Promotion)
				.WithMany(c => c.PromotionCategories)
				.HasForeignKey(c => c.PromotionId)
				.HasConstraintName("FK_PROMOTIONCATEGORY_PROMOTION_PROMOTIONID")
				.IsRequired();
			modelBuilder.Entity<PromotionCategory>()
				.HasOne(c => c.Category)
				.WithMany(c => c.PromotionCategories)
				.HasForeignKey(c => c.CategoryId)
				.HasConstraintName("FK_PROMOTIONCATEGORY_CATEGORY_CATEGORYID")
				.IsRequired();
			#endregion
			#region Table PromotionProducts
			modelBuilder.Entity<PromotionProduct>()
				.HasKey(pp => new { pp.PromotionId, pp.ProductId });
			modelBuilder.Entity<PromotionProduct>()
				.HasOne(c => c.Promotion)
				.WithMany(c => c.PromotionProducts)
				.HasForeignKey(c => c.PromotionId)
				.HasConstraintName("FK_PROMOTIONPRODUCT_PROMOTION_PROMOTIONID")
				.IsRequired();
			modelBuilder.Entity<PromotionProduct>()
				.HasOne(c => c.Product)
				.WithMany(c => c.PromotionProducts)
				.HasForeignKey(c => c.ProductId)
				.HasConstraintName("FK_PROMOTIONPRODUCT_PRODUCT_PRODUCTID")
				.IsRequired();
			#endregion
			#region  Table Reviews
			modelBuilder.Entity<Review>()
				.HasKey(c => c.ReviewId);
			modelBuilder.Entity<Review>()
				.Property(c => c.ReviewId)
				.ValueGeneratedOnAdd();
			modelBuilder.Entity<Review>()
				.HasOne(c => c.User)
				.WithMany(c => c.Reviews)
				.HasForeignKey(c => c.UserId)
				.HasConstraintName("FK_REVIEW_USER_USERID")
				.OnDelete(DeleteBehavior.NoAction)
				.IsRequired();
			modelBuilder.Entity<Review>()
				.HasOne(c => c.Product)
				.WithMany(c => c.Reviews)
				.HasForeignKey(c => c.ProductId)
				.HasConstraintName("FK_REVIEW_PRODUCT_PRODUCTID")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			modelBuilder.Entity<Review>()
				.HasOne(c => c.Order)
				.WithMany(c => c.Reviews)
				.HasForeignKey(c => c.OrderId)
				.HasConstraintName("FK_REVIEW_ORDER_ORDERID")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			modelBuilder.Entity<Review>()
				.Property(c => c.Rating)
				.HasColumnType("int")
				.IsRequired();
			modelBuilder.Entity<Review>()
				.ToTable(tb => tb.HasCheckConstraint("CK_Review_Rating", "Rating BETWEEN 1 AND 5"));
			modelBuilder.Entity<Review>()
				.Property(c => c.Comments)
				.HasMaxLength(2000);
			modelBuilder.Entity<Review>()
				.Property(c => c.ReviewTitle)
				.HasMaxLength(255);
			#endregion
			#region Table Roles
			modelBuilder.Entity<Role>()
				.HasKey(c => c.RoleId);
			modelBuilder.Entity<Role>()
				.Property(c => c.RoleId)
				.ValueGeneratedOnAdd();
			modelBuilder.Entity<Role>()
				.Property(c => c.RoleName)
				.IsRequired()
				.HasConversion<string>();
			modelBuilder.Entity<Role>()
				.Property(c => c.Description)
				.HasMaxLength(255);
			#endregion
			#region Table Seeds
			modelBuilder.Entity<Seed>()
				.HasKey(c => c.SeedId);
			modelBuilder.Entity<Seed>()
				.Property(c => c.SeedId)
				.ValueGeneratedOnAdd();
			modelBuilder.Entity<Seed>()
				.Property(c => c.THCContent)
				.HasPrecision(5, 2)
				.IsRequired();
			modelBuilder.Entity<Seed>()
				.Property(c => c.CBDContent)
				 .HasPrecision(5, 2)
				.IsRequired();
			modelBuilder.Entity<Seed>()
				.Property(c => c.StrainType)
				.HasConversion<string>();
			modelBuilder.Entity<Seed>()
				.HasOne(c => c.Classification)
				.WithMany(c => c.Seeds)
				.HasForeignKey(c => c.ClassifyId)
				.HasConstraintName("FK_SEED_CLASSIFICATION_CLASSIFYID")
				.IsRequired();
			modelBuilder.Entity<Seed>()
				.Property(c => c.FloweringTimeDays)
				.HasColumnType("INT");
			modelBuilder.Entity<Seed>()
				.Property(c => c.Yield)
				.HasPrecision(10, 2);
			modelBuilder.Entity<Seed>()
				.Property(c => c.Difficulty)
				.HasConversion<string>()
				.IsRequired();
			modelBuilder.Entity<Seed>()
				.Property(c => c.Price)
				.HasPrecision(10, 2)
				.IsRequired();
			modelBuilder.Entity<Seed>()
				.Property(c => c.IndicaPercentage)
				.HasPrecision(5, 2)
				.IsRequired();
			modelBuilder.Entity<Seed>()
				.Property(c => c.SativaPercentage)
				.HasPrecision(5, 2)
				.IsRequired();
			modelBuilder.Entity<Seed>()
				.Property(c => c.TotalQuantity)
				.HasColumnType("INT");
			modelBuilder.Entity<Seed>()
				.Property(c => c.Description)
				.HasMaxLength(1000);
			modelBuilder.Entity<Seed>()
				.Property(c => c.ProductId).IsRequired();
			modelBuilder.Entity<Seed>()
				.HasOne(c => c.Product)
				.WithOne(c => c.Seed)
				.HasForeignKey<Seed>(c => c.ProductId)
				.HasConstraintName("FK_SEED_PRODUCT_PRODUCTID")
				.OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<Seed>().HasIndex(g => g.ProductId)
				.HasDatabaseName("IX_GrowLight_ProductId");
			#endregion
			#region Table ShippingMedthods
			modelBuilder.Entity<ShippingMethod>(entity =>
			{
				entity.HasKey(c => c.ShippingId);
				entity.Property(c => c.ShippingId).ValueGeneratedOnAdd();
				entity.Property(c => c.Name).HasMaxLength(150).IsRequired();
				entity.Property(c => c.Carrier).HasMaxLength(150).IsRequired();
				entity.Property(c => c.EstimatedDeliveryDate).HasColumnType("datetime2").IsRequired();
				entity.Property(c => c.EstimatedDeliveryDays).HasColumnType("int").HasDefaultValue(0).IsRequired();
				entity.HasOne(c => c.Order).WithOne(c => c.ShippingMethod).HasForeignKey<ShippingMethod>(c => c.OrderId).HasConstraintName("FK_SHIPPINGMETHOD_ORDER_ORDERID").IsRequired();
				entity.HasIndex(c => c.OrderId).HasDatabaseName("IX_ShippingMethod_OrderId");
			});
			#endregion
			#region Table Spectrums
			modelBuilder.Entity<Spectrum>(entity =>
			{
				entity.HasKey(c => c.SpectrumId);
				entity.Property(c => c.SpectrumId).ValueGeneratedOnAdd();
				entity.Property(c => c.Type).HasConversion<string>().IsRequired();
				entity.Property(c => c.Description).HasMaxLength(1000);
			});

			#endregion
			#region Table ResfreshToken
			modelBuilder.Entity<RefreshToken>(entity =>
			{
				entity.HasKey(c => c.Id);
				entity.Property(c => c.Id)
					.ValueGeneratedOnAdd();

				entity.Property(c => c.RefreshTokenValue)
				  .IsRequired()
				  .HasMaxLength(256);
				entity.Property(r => r.ExpiresAt)
				   .IsRequired();
				entity.Property(r => r.IsRevoked)
				.IsRequired()
				.HasDefaultValue(false);
				// Quan hệ với User
				entity.HasOne(r => r.User)
					.WithMany(c => c.RefreshTokens)
					.HasForeignKey(r => r.UserId)
					.OnDelete(DeleteBehavior.Cascade);
				entity.Property(r => r.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
			});
			#endregion
			#region Table Users
			modelBuilder.Entity<User>(entity =>
			{
				entity.ToTable("Users", "Users");
				entity.HasKey(u => u.UserId);
				entity.Property(u => u.Username)
					  .IsRequired()
					  .HasMaxLength(100);
				entity.HasIndex(u => u.Username).IsUnique();
				entity.Property(u => u.HashPassword);
				entity.Property(u => u.Name)
					  .IsRequired()
					  .HasMaxLength(50);
				entity.Property(u => u.Email)
					  .IsRequired();
				entity.HasIndex(u => u.Email).IsUnique();
				entity.Property(u => u.PhoneNumber);
				entity.Property(u => u.Status)
					  .HasColumnType("nvarchar(20)")
					  .HasDefaultValue(EUserStatus.Active);
				entity.Property(u => u.RoleId)
					  .IsRequired();
				entity.HasOne(c => c.Role).WithMany(c => c.Users).HasForeignKey(c => c.RoleId).HasConstraintName("FK_USER_ROLE_ROLEID").OnDelete(DeleteBehavior.Restrict);

			});
			#endregion
		}


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{

		}
	}
}