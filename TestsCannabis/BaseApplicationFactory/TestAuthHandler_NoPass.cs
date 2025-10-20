using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace TestsCannabis.BaseApplicationFactory
{
	public class TestAuthHandler_NoPass : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		public TestAuthHandler_NoPass(
			IOptionsMonitor<AuthenticationSchemeOptions> options,
			ILoggerFactory logger,
			UrlEncoder encoder)
			: base(options, logger, encoder)
		{
			
		}

		protected override Task<AuthenticateResult> HandleAuthenticateAsync()
		{
		
			return Task.FromResult(AuthenticateResult.NoResult());
		}
	}
}

