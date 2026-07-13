using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations.SchemaInventory
{
	public class StockMovementTypeConfiguration : IEntityTypeConfiguration<StockMovementType>
	{
		public void Configure(EntityTypeBuilder<StockMovementType> builder)
		{
			// Đưa vào schema Inventory để đồng bộ với cụm quản lý kho
			builder.ToTable("StockMovementTypes", "Inventory");

			builder.HasKey(t => t.Id);
			builder.Property(t => t.Id).ValueGeneratedOnAdd();

			// Cấu hình trường Tên loại di chuyển kho (Ví dụ: Nhập kho, Xuất kho, Kiểm kê, Trả hàng)
			builder.Property(t => t.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(t => t.Description)
				.HasMaxLength(500);
		}
	}
}
