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
			Pending = 0,
			Confirmed = 1,
			Processing = 2,
			Shipped = 3,
			Delivered = 4,
			Canceled = 5,
			Returned = 6,
			Failed = 7
		}

		public enum ECartStatus
		{
			Active = 0,
			CheckedOut = 1,
			Abandoned = 2 // User thêm hàng nhưng 1 tuần không mua
		}

		public enum EDiscountType
		{
			Percent = 0, // Giảm %
			Fixed = 1    // Giảm số tiền cụ thể
		}
	}
}
