using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOs.User.Users
{
	public class UserFilterDTO
	{
		public string? SearchTerm { get; set; } // Tìm theo Name, Username, Email
		public int? RoleId { get; set; }       // Lọc theo Role
		public EUserStatus? Status { get; set; } // Lọc theo trạng thái (Active/Blocked)

		// Pagination params
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 10;
	}
}
