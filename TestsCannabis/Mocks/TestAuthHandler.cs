using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace TestsCannabis.Mocks
{
	public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
			ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
		{
			
		}

		protected override Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			// Đọc UserId và Role từ Header mà chúng ta sẽ gửi từ bài Test
			var userId = Context.Request.Headers["TestUserId"].FirstOrDefault() ?? "1";
			var role = Context.Request.Headers["TestRole"].FirstOrDefault() ?? "Admin";

			var claims = new[] {
				new Claim(ClaimTypes.NameIdentifier, userId),
				new Claim(ClaimTypes.Name, "TestUser"),
				new Claim(ClaimTypes.Role, role)
			};

			var identity = new ClaimsIdentity(claims, "TestScheme");
			var principal = new ClaimsPrincipal(identity);
			
			Context.User = principal;

			var ticket = new AuthenticationTicket(principal, "TestScheme");
			return Task.FromResult(AuthenticateResult.Success(ticket));
		}
	}
}
