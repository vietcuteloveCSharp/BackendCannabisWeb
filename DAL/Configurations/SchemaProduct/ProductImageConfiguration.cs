using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations.SchemaProduct
{
	public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
	{
		public void Configure(EntityTypeBuilder<ProductImage> builder)
		{
			builder.ToTable("ProductImages", "Products");
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).ValueGeneratedOnAdd();
			builder.Property(c => c.ImageUrl).IsRequired();
			builder.Property(i => i.AltText).HasMaxLength(300);
			builder.Property(c => c.IsMainImage).HasDefaultValue(false);

			builder.HasOne(c => c.Product)
				   .WithMany(c => c.ProductImages)
				   .HasForeignKey(c => c.ProductId)
				   .HasConstraintName("FK_PRODUCTIMAGE_PRODUCT_PRODUCTID")
				   .OnDelete(DeleteBehavior.Cascade)
				   .IsRequired();
			// Quan hệ với bảng ProductVariant (Xóa một Variant cụ thể thì ảnh của Variant đó tự động set NULL hoặc xóa)
			// Dùng Restrict hoặc SetNull tùy thuộc vào việc bạn có muốn giữ lại ảnh khi Variant bị xóa không. 
			// Ở đây dùng Restrict để chặn việc xóa nhầm variant khi đang có ảnh ràng buộc.
			builder.HasOne(pi => pi.ProductVariant)
				.WithMany(pv => pv.ProductImages) // Đảm bảo trong ProductVariant.cs cũng có: public virtual ICollection<ProductImage> ProductImages { get; set; } = new HashSet<ProductImage>();
				.HasForeignKey(pi => pi.ProductVariantId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_ProductImages_ProductVariants_ProductVariantId");
		}
	}
	
}
