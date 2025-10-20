using Xunit.Abstractions;

namespace TestsCannabis.TestAPI.AuthControllerTests
{	
	public class TestLogout_Logic :IClassFixture<CannabisWebApplicationFactory>
	{
		private readonly HttpClient _client;
		private readonly HttpClient _adminClient;
		private readonly HttpClient _userClient;
		private readonly ITestOutputHelper _output;
		private readonly FakeAuthFactory_Admin _adminFactory;
		private readonly FakeAuthFactory_User _userFactory;
		public TestLogout_Logic(CannabisWebApplicationFactory factory, ITestOutputHelper output)
		{
			_output = output;
			_client = factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				BaseAddress = new Uri("https://localhost/api/v1/")
			});
			_adminFactory = new FakeAuthFactory_Admin();
			_adminClient = _adminFactory.CreateClient(new WebApplicationFactoryClientOptions
			{
				BaseAddress = new Uri("https://localhost/api/v1/")
			});
			_userFactory = new FakeAuthFactory_User();
			_userClient = _userFactory.CreateClient(new WebApplicationFactoryClientOptions
			{
				BaseAddress = new Uri("https://localhost/api/v1/")
			});

		}
		[Fact(DisplayName = "Logout_ShouldReturn200_WhenValidToken")]
		public async Task Logout_ShouldReturn200_WhenValidToken()
		{
			// Arrange
			var refreshToken = "valid_refresh_token";

			// Act
			var response = await _adminClient.PostAsJsonAsync("Auth/logout", refreshToken);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			var content = await response.Content.ReadAsStringAsync();
			content.Should().Contain("Refresh token successfully revoked");
		}
	}

}
