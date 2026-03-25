
namespace DAL.Entities
{
    public class Spectrum : BaseEntity
    {
        [Key]
        public int SpectrumId { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public ESpectrumType Type { get; set; }
		public string? ColorHexCode { get; set; } // Màu đại diện (ví dụ: Hồng tím cho Bloom)
		public string? SpectrumChartUrl { get; set; } // Ảnh biểu đồ bước sóng
		public string? Description { get; set; }
		public virtual ICollection<GrowLight> GrowLights { get; set; } = new HashSet<GrowLight>();

		
	}
}
