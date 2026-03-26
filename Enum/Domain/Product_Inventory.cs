using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enum.Domain
	{ 
	public static class Product_Inventory
	{
		public enum EProductType
		{
			Growlight = 1,
			Seed = 2,
			Growtent = 3,
			CarbonFilter = 4,
			Nutrient = 5,
			Dehumidifier = 6
		}
		public enum EPowerSypplyType
		{
			Internal = 1,   // Nguồn tích hợp trong thân đèn (gọn nhưng tỏa nhiệt lên chip)
			External = 2,   // Nguồn rời, có dây nối dài (giúp giảm nhiệt trong lều)
			Removable = 3,  // Nguồn gắn trên đèn nhưng có thể tháo rời nếu muốn
			Driverless = 4  // Công nghệ mới (AC Direct) không cần cục Driver cồng kềnh

		}
		public enum EStrainType
		{
			Indica = 1,
			Sativa = 2,
			Hybrid = 3,
			Autoflower = 4
		}

		public enum EDifficulty
		{
			Easy = 1,
			Medium = 2,
			Hard = 3
		}

		public enum ESpectrumType
		{
			FullSpectrum = 1, // Phổ đầy đủ (Dùng cho mọi giai đoạn)
			Vegetative = 2,   // Phổ xanh dương (Chuyên cho giai đoạn phát triển lá)
			Flowering = 3,    // Phổ đỏ (Chuyên cho giai đoạn tạo hoa/quả)
			UV_Supplement = 4,// Tia cực tím (Bổ sung để tăng nhựa/đề kháng)
			IR_Supplement = 5,// Tia hồng ngoại (Bổ sung để kích thích ra hoa nhanh)
			DualSpectrum = 6, // Kết hợp cả Veg và Bloom
			Customized = 7    // Phổ có thể điều chỉnh qua App/Controller
		}
		public enum EApplicationStage
		{
			AllPurpose = 1,
			Seedling = 2,    // Cây con
			Vegetative = 3,  // Sinh trưởng
			Flowering = 4,   // Ra hoa
			Flushing = 5     // Xả phân
		}
		public enum ESeedClassify
		{
			Feminized = 1, // Hạt cái (99% ra cây cái)
			Regular = 2,   // Hạt tự nhiên (có cả đực và cái)
			FastVersion = 3 // Dòng nở sớm
		}
	}
}
