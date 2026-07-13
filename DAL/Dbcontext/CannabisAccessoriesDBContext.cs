using Shared.Common.Inherited;
using Shared.DTOs.Common.Extension;
using Shared.DTOs.DTO.AuditLog;
using System.Text.Json;

namespace DAL.Dbcontext
{
	public class CannabisAccessoriesDBContext : DbContext
	{
		private readonly IAuditQueue? _auditQueue;
		public CannabisAccessoriesDBContext(
		 DbContextOptions<CannabisAccessoriesDBContext> options)
		:base(options)
		{
		}
		public CannabisAccessoriesDBContext(DbContextOptions<CannabisAccessoriesDBContext> options,IAuditQueue auditQueue) : base(options)
		{
			_auditQueue = auditQueue;
		}
		// --- PHÂN HỆ NỘI BỘ QUẢN TRỊ (Internal Schema) ---
		public virtual DbSet<Staff> Staffs { get; set; } // Đã đổi từ Users thành Staffs[cite: 6, 44]
		public virtual DbSet<StaffStatus> StaffStatuses { get; set; } // Sửa tên biến cho đồng bộ[cite: 47]
		public virtual DbSet<StaffSession> StaffSessions { get; set; } // Tách biệt Session của nhân viên[cite: 46]
		public virtual DbSet<StaffRefreshToken> StaffRefreshTokens { get; set; } // Tách biệt Token của nhân viên[cite: 45]

		// --- PHÂN HỆ KHÁCH HÀNG & MUA SẮM (Shop Schema) ---
		public virtual DbSet<Customer> Customers { get; set; } // Bảng Customer mới tinh[cite: 38]
		public virtual DbSet<CustomerSession> CustomerSessions { get; set; } // Tách biệt Session của khách hàng[cite: 40]
		public virtual DbSet<CustomerRefreshToken> CustomerRefreshTokens { get; set; } // Tách biệt Token của khách hàng[cite: 39]
		public virtual DbSet<Address> Addresses { get; set; }
		public virtual DbSet<Cart> Carts { get; set; }
		
		public virtual DbSet<CartItem> CartItems { get; set; }
		public virtual DbSet<Wishlist> Wishlists { get; set; }
		public virtual DbSet<Review> Reviews { get; set; }

		// --- PHÂN HỆ ĐƠN HÀNG (Order Subsystem) ---
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<OrderItem> OrderItems { get; set; }
		public virtual DbSet<OrderHistory> OrderHistories { get; set; } // Sửa tên biến thành số nhiều cho chuẩn
		public virtual DbSet<OrderStatus> OrderStatuses { get; set; } // Sửa tên biến thành số nhiều cho chuẩn

		// --- PHÂN HỆ THÔNG BÁO (Noti Schema - THÊM MỚI) ---
		public virtual DbSet<Notification> Notifications { get; set; } // Bảng nội dung thông báo gốc[cite: 49]
		public virtual DbSet<CustomerNotificationLog> CustomerNotificationLogs { get; set; } // Log thông báo khách[cite: 48]
		public virtual DbSet<StaffNotificationLog> StaffNotificationLogs { get; set; } // Log thông báo nhân viên[cite: 50]

		// --- PHÂN HỆ PHÂN QUYỀN CHUNG (User/System Schema) ---
		public virtual DbSet<Role> Roles { get; set; }

		// --- PHÂN HỆ SẢN PHẨM & KHO HÀNG (Giữ nguyên cấu trúc của bạn) ---
		public virtual DbSet<Brand> Brands { get; set; }
		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<ProductImage> ProductImages { get; set; }
		public virtual DbSet<ProductVariant> ProductVariants { get; set; }
		public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
		public virtual DbSet<AttributeValue> AttributeValues { get; set; }
		public virtual DbSet<ProductVariantAttribute> ProductVariantAttributes { get; set; }
		public virtual DbSet<Tag> Tags { get; set; }
		public virtual DbSet<ProductTag> ProductTags { get; set; }
		public virtual DbSet<Inventory> Inventories { get; set; }
		public virtual DbSet<StockMovement> StockMovements { get; set; } // Sửa tên biến thành số nhiều

		// --- PHÂN HỆ KHUYẾN MÃI (Promotion Subsystem) ---
		public virtual DbSet<Promotion> Promotions { get; set; }
		public virtual DbSet<PromotionCategory> PromotionCategories { get; set; }
		public virtual DbSet<PromotionProduct> PromotionProducts { get; set; }
		public virtual DbSet<Coupon> Coupons { get; set; }
		public virtual DbSet<CouponUsage> CouponUsages { get; set; }

		// --- PHÂN HỆ THANH TOÁN & VẬN CHUYỂN (Payment & Shipment) ---
		public virtual DbSet<Payment> Payments { get; set; }
		public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
		public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }
		public virtual DbSet<ShippingMethod> ShippingMethods { get; set; }
		public virtual DbSet<Shipment> Shipments { get; set; }
		public virtual DbSet<ShipmentItem> ShipmentItems { get; set; }
		public virtual DbSet<ShipmentStatus> ShipmentStatuses { get; set; }



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// 1. Tự động apply các file Configuration thuộc namespace DAL.Configurations
			modelBuilder.ApplyConfigurationsFromNamespace(typeof(CannabisAccessoriesDBContext).Assembly, "DAL.Configurations");

			// 2. Tự động apply Query Filter cho Soft Delete
			modelBuilder.ApplySoftDeleteQueryFilter();


		}


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{

		}

		// lưu theo entity state 
		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			// BƯỚC 1: Duyệt qua các Entity để tự động điền Metadata & Xử lý Soft Delete trước
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
							// Đổi trạng thái từ Xóa sang Sửa (Soft Delete)
							entry.State = EntityState.Modified;
							softDelete.IsDeleted = true;
							softDelete.DeletedAt = DateTime.UtcNow;
						}
						break;
				}
			}

			// BƯỚC 2: Chụp lại trạng thái chính xác của các Entity sau khi đã chạy xong vòng lặp ở bước 1
			var auditEntries = CaptureAuditEntries();

			// BƯỚC 3: Thực hiện lưu dữ liệu kinh doanh chính xuống DB gốc
			var result = await base.SaveChangesAsync(cancellationToken);

			// BƯỚC 4: Nếu lưu thành công (result > 0) và có log, đẩy toàn bộ log vào hàng đợi ở tầng Shared
			if (_auditQueue != null && result > 0 && auditEntries.Any())
			{
				foreach (var entry in auditEntries)
				{
					_auditQueue.QueueAuditLog(entry); // Kích hoạt đường ống dẫn sang DB Audit!
				}
			}

			return result;
		}
		private List<AuditLogDTO> CaptureAuditEntries()
		{
			// 1. Ép EF Core cập nhật và đồng bộ lại toàn bộ trạng thái thay đổi của các Entity hiện tại
			ChangeTracker.DetectChanges();

			var auditEntries = new List<AuditLogDTO>();

			// 2. Duyệt qua tất cả các thực thể (Entities) đang được EF Core theo dõi (Tracking)
			foreach (var entry in ChangeTracker.Entries())
			{
				// Nếu thực thể không thuộc diện Thêm/Sửa/Xóa (chỉ Đọc hoặc bị ngắt kết nối) thì bỏ qua, không log
				if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
					continue;

				// 3. Khởi tạo đối tượng DTO chứa thông tin log cơ bản của thực thể hiện tại
				var auditEntry = new AuditLogDTO
				{
					UserId = null, // 💡 Sẽ cấu hình lấy ID từ Claims/CurrentUser sau khi dựng xong Middleware Auth
					TableName = entry.Metadata.GetTableName() ?? "Unknown", // Lấy tên bảng thực tế dưới Database
					Action = entry.State.ToString(), // Lưu trạng thái thao tác: Added, Modified, hoặc Deleted
					ActionTime = DateTime.UtcNow // Ghi lại mốc thời gian UTC chính xác xảy ra hành động
				};

				// Khởi tạo các bộ từ điển (Dictionary) để gom dữ liệu cột dưới dạng Key-Value
				var keyDict = new Dictionary<string, object>();       // Chứa các cột Khóa chính (Primary Key)
				var oldDict = new Dictionary<string, object>();       // Chứa giá trị CŨ trước khi sửa/xóa
				var newDict = new Dictionary<string, object>();       // Chứa giá trị MỚI sau khi thêm/sửa
				var changedCols = new List<string>();                // Danh sách tên các cột thực sự bị thay đổi

				// 4. Duyệt qua từng thuộc tính (Cột/Property) của thực thể hiện tại
				foreach (var property in entry.Properties)
				{
					string propertyName = property.Metadata.Name; // Lấy tên cột trong Entity (ví dụ: "Price", "Username")

					// 🛡️ BẢO MẬT: Tuyệt đối không bao giờ ghi lại chuỗi băm mật khẩu vào DB Audit Log
					if (propertyName == "PasswordHash")
						continue;

					// 🔑 Nếu thuộc tính này là Khóa chính (Primary Key)
					if (property.Metadata.IsPrimaryKey())
					{
						// Lưu lại thông tin khóa chính để biết bản ghi nào bị tác động (Ví dụ: Id = 5)
						keyDict[propertyName] = property.CurrentValue ?? "";
						continue; // Xử lý xong khóa chính thì nhảy sang thuộc tính tiếp theo
					}

					// 5. Phân loại bóc tách dữ liệu dựa trên hành động (EntityState)
					switch (entry.State)
					{
						// Trường hợp THÊM MỚI (Insert)
						case EntityState.Added:
							// Thêm mới thì chỉ có giá trị MỚI, giá trị cũ bằng rỗng
							newDict[propertyName] = property.CurrentValue ?? "";
							break;

						// Trường hợp XÓA CỨNG (Delete hẳn dòng khỏi DB)
						case EntityState.Deleted:
							// Xóa đi thì chỉ cần giữ lại giá trị CŨ trước khi mất để đối chiếu
							oldDict[propertyName] = property.OriginalValue ?? "";
							break;

						// Trường hợp CẬP NHẬT (Update)
						case EntityState.Modified:
							// 💡 Điểm tối ưu: Chỉ ghi log nếu cột này thực sự bị người dùng thay đổi giá trị
							if (property.IsModified)
							{
								changedCols.Add(propertyName); // Nhét tên cột vào danh sách biến động (ví dụ: "StatusId")
								oldDict[propertyName] = property.OriginalValue ?? ""; // Hốt giá trị cũ trong DB ra
								newDict[propertyName] = property.CurrentValue ?? ""; // Hốt giá trị mới chuẩn bị lưu xuống
							}
							break;
					}
				}

				// 6. Chuyển đổi các bộ Dictionary dữ liệu thô sang chuỗi JSON string để map vào Dto
				// Nếu bộ từ điển nào có dữ liệu mới tiến hành Serialize, ngược lại để Null cho nhẹ DB Audit
				auditEntry.KeyValues = keyDict.Any() ? JsonSerializer.Serialize(keyDict) : null;
				auditEntry.OldValues = oldDict.Any() ? JsonSerializer.Serialize(oldDict) : null;
				auditEntry.NewValues = newDict.Any() ? JsonSerializer.Serialize(newDict) : null;
				auditEntry.ChangedColumns = changedCols.Any() ? JsonSerializer.Serialize(changedCols) : null;

				// 7. Thêm cục Log hoàn chỉnh của thực thể này vào danh sách tổng
				auditEntries.Add(auditEntry);
			}

			// Trả về danh sách chứa toàn bộ log đã bóc tách để chuẩn bị ném vào Queue ngầm sang DB Audit
			return auditEntries;
		}
	}
}
