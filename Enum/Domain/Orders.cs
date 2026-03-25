using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enum.Domain
{
	public static class Orders
	{

		public enum EOrderStatus
		{
			Pending = 0,    // Chờ thanh toán/xác nhận
			Confirmed = 1,  // Đã xác nhận đơn
			Processing = 2, // Đang đóng gói
			Shipped = 3,    // Đã giao cho đơn vị vận chuyển
			Delivered = 4,  // Giao hàng thành công
			Canceled = 5,   // Đã hủy
			Returned = 6,   // Khách trả hàng
			Failed = 7,     // Giao hàng thất bại
			Refunded = 8    // Đã hoàn tiền
		}

		public enum ECartStatus
		{
			Active = 0,
			CheckedOut = 1,
			Abandoned = 2  // Giỏ hàng bị bỏ quên
		}

		public enum EDiscountType
		{
			Percentage = 0, // Giảm theo % (ví dụ 10%)
			FixedAmount = 1 // Giảm số tiền cụ thể (ví dụ 50k)
		}
	}
}
