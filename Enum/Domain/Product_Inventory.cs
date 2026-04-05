using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enum.Domain
	{ 
	public static class Product_Inventory
	{
		public enum EStockMovementType
		{
			Inbound = 1, // Nhập kho
			Outbound = 2, // Xuất kho
			Adjustment = 3 // Điều chỉnh
		}
	}
}
