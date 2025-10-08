namespace Enum.EnumableClass
{
    public static class EnumableClass
    {
        public enum ECoolingType
        {
            Active,
            Passive
        }
        public enum EUserStatus
        {
            Active,   // Đang hoạt động
            Inactive,   // Không hoạt động
            Suspended,  // Đã bị khóa
          
           
        }
		public enum ECartStatus
		{
			Active = 0,       // đang sử dụng
			CheckedOut = 1,   // đã thanh toán
			Abandoned = 2     // người dùng bỏ giỏ
		}
		public enum EOrderSatus
        {
            Pending,         // Đang chờ xử lý
            Confirmed,       // Đã xác nhận
            Processing,      // Đang xử lý
            Shipped,         // Đã giao cho đơn vị vận chuyển
            Delivered,       // Đã giao hàng thành công
            Canceled,        // Đã hủy
            Returned,        // Khách hàng đã trả lại hàng
            Failed           // Giao hàng thất bại
        }
        public enum EPowerSypplyType
        {
            AC,
            DC,
            Battery
        }
        public enum EDiscountType
        {
            Percent,
            Fixed
        }
        public enum ERoleName
        {
            Admin = 0,
            Employee = 1,
            User=2
        }
        public enum EDifficulty
        {
            Easy,
            Medium,
            Hard
        }
        public enum EStrainType
        {

            Indica,
            Sativa,
            Hybrid

        }
        public enum ESellerStatus
        {
            Active,
            NonActive
        }
        public enum ESpectrumType
        {
            FullSpectrum,
            RedBlue,
            WarmWhite,
            CoolWhite,
            Uv
        }
        public enum EProductType
        {
            Growlight,Seed,Growtent,CarbonFilter,Nutrient, Dehumidifier

        }
        public enum EActionLog
        {
            Insert,
            Update, 
            Delete,
            Soft_Delete
		}

	 }
}
