using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Service.IServices.IServicesAuth;
using Service.IServices.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
	public class JwtMiddleware
	{
		private readonly RequestDelegate _next;
		//private readonly IConfiguration _configuration;
		//private readonly ITokenService _tokenSerivce;
		//private readonly ISellerService _sellerSerivce;
		//private readonly ICustomerService _customerSerivce;
		public JwtMiddleware(RequestDelegate next)
		{
			this._next = next;
			

		}
		public async Task Invoke(HttpContext context, IConfiguration configuration, ITokenService tokenSerivce, IUserService userService)
		{
			var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

			if (!string.IsNullOrEmpty(token))
			{
				var principal = tokenSerivce.ValidateToken(token);
				
				if (principal != null)
				{
					// Lấy UserId by claim
					var userIdStr = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
					// get value của UserType từ claims
					var role = principal.FindFirst(ClaimTypes.Role)?.Value;
					// Gắn user vào context để controller có thể dùng
					if (int.TryParse(userIdStr, out var userId))
					{
						var user = await userService.GetUserByIdAsync(userId);

						if (user != null)
						{
							context.Items["User"] = user;

							if (!string.IsNullOrEmpty(role))
								context.Items["Role"] = role;
						}
					}
				}
			}

			await _next(context); // tiếp tục middleware pipeline
		}
	}
}
