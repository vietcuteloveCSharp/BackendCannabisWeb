using Xunit.Abstractions;

namespace TestsCannabis.TestAPI.AuthControllerTests
{
	public class TestLogout_EdgeCase :IClassFixture<CannabisWebApplicationFactory>
	{
		private readonly HttpClient _client;
		private readonly HttpClient _adminClient;
		private readonly HttpClient _userClient;
		private readonly ITestOutputHelper _output;
		private readonly FakeAuthFactory_Admin _adminFactory;
		private readonly FakeAuthFactory_User _userFactory;
		public TestLogout_EdgeCase(CannabisWebApplicationFactory factory, ITestOutputHelper output)
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
		[Fact(DisplayName = "Logout_ShouldReturn401_WhenNoToken")]
		public async Task Logout_ShouldReturn401_WhenNoToken()
		{
			// Act
			var response = await _client.PostAsJsonAsync("Auth/logout", "fake_token");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
		}
		[Theory(DisplayName = "Logout_ShouldReturn400_WhenTokenInvalid")]
		[InlineData("")]
		[InlineData("   ")]
		public async Task Logout_ShouldReturn400_WhenTokenInvalid(string? refreshToken)
		{
			// Act
			var response = await _adminClient.PostAsJsonAsync("Auth/logout", refreshToken);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			var content = await response.Content.ReadAsStringAsync();
			content.Should().Contain("Refresh token is required");
			
		}

	}
}
