using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace TestsCannabis.BaseApplicationFactory
{   // Giả lập lập role Employee
	public class TestAuthHandler_Employee:AuthenticationHandler<AuthenticationSchemeOptions>
	{
		public TestAuthHandler_Employee(
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
				new Claim(ClaimTypes.Name, "TestEmployee"),
				new Claim(ClaimTypes.Role, "Employee"),
			};

			var identity = new ClaimsIdentity(claims, Scheme.Name);
			var principal = new ClaimsPrincipal(identity);
			var ticket = new AuthenticationTicket(principal, Scheme.Name);

			return Task.FromResult(AuthenticateResult.Success(ticket));
		}
	}
}
