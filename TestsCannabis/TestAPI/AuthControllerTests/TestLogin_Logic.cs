using System.Text.Json;

namespace TestsCannabis.TestAPI.AuthControllerTests
{
	public class TestLogin_Logic : IClassFixture<CannabisWebApplicationFactory>
	{
		private readonly HttpClient _client;

		public TestLogin_Logic(CannabisWebApplicationFactory factory)
		{
			_client = factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				BaseAddress = new Uri("https://localhost/api/v1/")
			});
		}

		[Fact(DisplayName = "POST auth/login - Return 200 and token when credentials valid")]
		public async Task Login_ShouldReturn200_WhenCredentialsValid()
		{
			// Arrange
			var dto = new LoginResquestDTO
			{
				Username = "testadmin01",   // user có sẵn từ DbSeeder
				Password = "Vuvietanh1!",
				RememberMe = false
			};

			// Act
			var response = await _client.PostAsJsonAsync("auth/login", dto);
			var content = await response.Content.ReadAsStringAsync();

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK, because: content);

			// Kiểm tra accessToken nằm trong phần "data"
			content.Should().Contain("accessToken", because: "response should include access token when login successful");
		}
		

		[Fact(DisplayName = "POST auth/login - Include refresh token when RememberMe = true")]
		public async Task Login_ShouldIncludeRefreshToken_WhenRememberMeTrue()
		{
			// Arrange
			var dto = new LoginResquestDTO
			{
				Username = "testadmin01",
				Password = "Vuvietanh1!",
				RememberMe = true
			};

			// Act
			var response = await _client.PostAsJsonAsync("auth/login", dto);
			var content = await response.Content.ReadAsStringAsync();

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK, because: content);

			content.Should().Contain("refreshToken",
				because: "response should include refresh token when RememberMe is true");
		}
	}
}


