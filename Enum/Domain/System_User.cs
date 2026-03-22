using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enum.Domain
{
	public static class System_User
	{
		public enum ERoleName
		{
			Admin = 1,
			Employee = 3,
			User = 2
		}
		public enum EUserStatus
		{
			Active = 0,
			Inactive = 1,
			Suspended = 2 // Dùng để test case: User bị khóa không được login
		}
	}
}
