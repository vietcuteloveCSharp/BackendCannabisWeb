using Microsoft.AspNetCore.Authentication;

namespace TestsCannabis.BaseApplicationFactory
{
	public class FakeAuthFactory_User : CannabisWebApplicationFactory
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			base.ConfigureWebHost(builder);

			builder.ConfigureServices(services =>
			{
				services.AddAuthentication("UserScheme")
					.AddScheme<AuthenticationSchemeOptions, TestAuthHandler_User>(
						"UserScheme", _ => { });
			});
		}
	}
}
