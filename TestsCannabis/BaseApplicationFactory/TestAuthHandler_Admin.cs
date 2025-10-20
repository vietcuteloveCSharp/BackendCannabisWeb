using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace TestsCannabis.BaseApplicationFactory
{
	// Giả lập lập role admin 
	public class TestAuthHandler_Admin : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		public TestAuthHandler_Admin(
			IOptionsMonitor<AuthenticationSchemeOptions> options,
			ILoggerFactory logger,
			UrlEncoder encoder)
			: base(options, logger, encoder)
		{
		}

		protected override Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			var claims = new[]
			{
				new Claim(ClaimTypes.Name, "TestAdmin"),
				new Claim(ClaimTypes.Role, "Admin"),
			};

			var identity = new ClaimsIdentity(claims, Scheme.Name);
			var principal = new ClaimsPrincipal(identity);
			var ticket = new AuthenticationTicket(principal, Scheme.Name);

			return Task.FromResult(AuthenticateResult.Success(ticket));
		}
	}
}
