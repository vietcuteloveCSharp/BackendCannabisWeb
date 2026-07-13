namespace DAL.Configurations.SchemaInventory
{
	public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
	{
		public void Configure(EntityTypeBuilder<StockMovement> builder)
		{
			builder.ToTable("StockMovements","Inventory");

			builder.HasKey(s => s.Id);

			builder.Property(s => s.QuantityChanged)
				.IsRequired();


			builder.Property(s => s.Note)
				.HasMaxLength(500);

			// Cấu hình quan hệ với bảng Inventories (Bảng quản lý tồn kho lõi)
			builder.HasOne(sm => sm.Inventory)
				.WithMany(sm=>sm.StockMovements) // Nếu bảng Inventory không cần list danh sách các lịch sử dịch chuyển
				.HasForeignKey(sm => sm.InventoryId)
				.OnDelete(DeleteBehavior.Restrict) // Không cho xóa Inventory nếu đã phát sinh lịch sử dịch kho
				.HasConstraintName("FK_StockMovements_Inventories_InventoryId");

			// Cấu hình mối quan hệ Khóa ngoại liên kết Động tới bảng loại di chuyển vừa tạo
			builder.HasOne(sm => sm.StockMovementType)
				.WithMany(t => t.StockMovements) // Chỉ định rõ quan hệ ngược lại từ phía bảng Type
				.HasForeignKey(sm => sm.TypeId)
				.OnDelete(DeleteBehavior.Restrict) // Chặn xóa Loại hình dịch chuyển nếu dữ liệu kho đang dùng
				.HasConstraintName("FK_StockMovements_StockMovementTypes_TypeId");

			builder.HasQueryFilter(s => !s.IsDeleted);
			builder.HasIndex(sm => sm.InventoryId);
			builder.HasIndex(sm => sm.TypeId);
		}
	}
}
