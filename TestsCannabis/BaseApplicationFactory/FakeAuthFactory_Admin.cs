using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsCannabis.BaseApplicationFactory
{
	public class FakeAuthFactory_Admin :CannabisWebApplicationFactory
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			base.ConfigureWebHost(builder);

			builder.ConfigureServices(services =>
			{
				// Xóa JWT thật
				services.AddAuthentication("AdminScheme")
					.AddScheme<AuthenticationSchemeOptions, TestAuthHandler_Admin>(
						"AdminScheme", options => { });
			});
		}
	}
}
