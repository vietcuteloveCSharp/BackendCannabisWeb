using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.TokenDTOs
{	//dùng cho api
	public class TokenResultDTO
	{
		public string AccessToken { get; set; } = default!;
		public DateTime ExpiresAt { get; set; }
		public UserSummaryDTO User { get; set; } = default!;
	}
}
