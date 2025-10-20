using Microsoft.AspNetCore.Authentication;

namespace TestsCannabis.BaseApplicationFactory
{
	public class FakeAuthFactory_Employee :CannabisWebApplicationFactory
	{
			protected override void ConfigureWebHost(IWebHostBuilder builder)
			{
				base.ConfigureWebHost(builder);

				builder.ConfigureServices(services =>
				{
					services.AddAuthentication("EmpolyeeScheme")
						.AddScheme<AuthenticationSchemeOptions, TestAuthHandler_Employee>(
							"EmpolyeeScheme", _ => { });
				});
			}
	}
}
