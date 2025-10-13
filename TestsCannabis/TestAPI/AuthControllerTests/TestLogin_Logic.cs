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

		[Fact(DisplayName = "Login - Should return 200 and token,refreshtoken when credentials valid with rememberMe = true")]
		public async Task Login_ShouldReturnTokenAndRefreshToken_WhenCredentialsValid()
		{
			// Arrange
			var dto = new LoginResquestDTO
			{
				Username = "testuser01",  // đã seed trong DB
				Password = "Vuvietanh1!",
				RememberMe= true
			};

			// Act
			var response = await _client.PostAsJsonAsync("auth/login", dto);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			response.Content.Headers.ContentType!.ToString()
				.Should().Contain("application/json");

			var result = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();

			result.Should().NotBeNull();
			result.Should().ContainKey("accessToken");
			result.Should().ContainKey("refreshToken");

			result["accessToken"].Should().NotBeNull();
			result["refreshToken"].Should().NotBeNull();
		}

		[Fact(DisplayName = "Login - Should return 200 and token when credentials valid with rememberMe = false")]
		public async Task Login_ShouldReturnToken_WhenCredentialsValid()
		{
			// Arrange
			var dto = new LoginResquestDTO
			{
				Username = "testuser01",  // đã seed trong DB
				Password = "Vuvietanh1!",
				RememberMe = false
			};
			// Act
			var response = await _client.PostAsJsonAsync("auth/login", dto);
			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			response.Content.Headers.ContentType!.ToString()
				.Should().Contain("application/json");

			var result = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();

			result.Should().NotBeNull();
			result.Should().ContainKey("accessToken");
			result.Should().ContainKey("refreshToken");

			result["accessToken"].Should().NotBeNull();
			result["refreshToken"].Should().BeNull();
		}
	}
}

