namespace DAL.Entities
{
    public class CarbonFilter :BaseEntity
    {
        [Key]
        public int Id { get; set; }
		public int ProductId { get; set; }
		public int BrandId { get; set; } // Mã thương hiệu
        // TỐI ƯU: Chuyển sang số để lọc (Ví dụ: 400 CFM)
        public int AirflowRateCFM { get; set; }
		// TỐI ƯU: Kích thước miệng nối cực quan trọng (4, 6, 8, 10, 12 inch)
		public decimal FlangeSizeInch { get; set; }
        public int Quantity { get; set; } =0;
        public decimal Price { get; set; } // Giá
        public string FilterMaterial { get; set; } = string.Empty; // Chất liệu lọc
		public decimal CarbonBedThicknessMm { get; set; } // Độ dày lớp than (càng dày lọc càng kỹ)
		public decimal Diameter { get; set; } // Đường kính
        public decimal Length { get; set; } // Chiều dài
        public int Lifespan { get; set; } // Tuổi thọ (giờ hoặc ngày)

        public decimal MinTemperature { get; set; } // Nhiệt độ tối thiểu
   
        public decimal MaxTemperature { get; set; } // Nhiệt độ tối đa
        public string? Description { get; set; } // Mô tả sản phẩm
        public int WarrantyPeriod { get; set; }//thời gian bảo hành
        public string ModelNumber { get; set; } = string.Empty; // Số model

        //navigation
        public virtual Brand Brand { get; set; } = default!;
        public virtual Product Product { get; set; } = default!;
    }
}
