using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dbcontext.Configurations
{
	public class SpectrumConfiguration : IEntityTypeConfiguration<Spectrum>
	{
		public void Configure(EntityTypeBuilder<Spectrum> builder)
		{
			builder.ToTable("Spectrums", "Inventory");
			builder.HasKey(c => c.SpectrumId);
			builder.Property(c => c.SpectrumId).ValueGeneratedOnAdd();

			builder.Property(c => c.Type)
				.HasConversion<string>()
				.IsRequired();

			builder.Property(c => c.Description)
				.HasMaxLength(1000);

			// Quan hệ ngược với GrowLight (nếu cần thiết, trong context bạn đã define ở phía GrowLight)
		}
	}
}
