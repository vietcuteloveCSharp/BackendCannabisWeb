
namespace TestsCannabis.TestAPI.AuthControllerTests
{
	public class TestLogin_EdgeCase : IClassFixture<CannabisWebApplicationFactory>
	{
		private readonly HttpClient _client;

		public TestLogin_EdgeCase(CannabisWebApplicationFactory factory)
		{
			_client = factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				BaseAddress = new Uri("https://localhost/api/v1/")
			});
		}

		[Fact(DisplayName = "Login - Should return 400 when username missing")]
		public async Task Login_ShouldReturn400_WhenUsernameMissing()
		{
			// Arrange
			var dto = new LoginResquestDTO { Username = "", Password = "123" };

			// Act
			var response = await _client.PostAsJsonAsync("auth/login", dto);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			var msg = await response.Content.ReadAsStringAsync();
			msg.Should().Contain("Username and password are required.");
		}

		[Fact(DisplayName = "Login - Should return 400 when password missing")]
		public async Task Login_ShouldReturn400_WhenPasswordMissing()
		{
			// Arrange
			var dto = new LoginResquestDTO { Username = "testuser01", Password = "",RememberMe=true };

			// Act
			var response = await _client.PostAsJsonAsync("auth/login", dto);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			var msg = await response.Content.ReadAsStringAsync();
			msg.Should().Contain("Username and password are required.");
		}

		[Fact(DisplayName = "Login - Should return 400 when wrong password")]
		public async Task Login_ShouldReturn400_WhenWrongPassword()
		{
			// Arrange
			var dto = new LoginResquestDTO
			{
				Username = "testuser01", // user seeded in DbSeeder
				Password = "WrongPassword!",
				RememberMe=false
			};

			// Act
			var response = await _client.PostAsJsonAsync("auth/login", dto);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			var msg = await response.Content.ReadAsStringAsync();
			msg.Should().Contain("Invalid");
		}

		[Fact(DisplayName = "Login - Should return 400 when user not found")]
		public async Task Login_ShouldReturn400_WhenUserNotFound()
		{
			// Arrange
			var dto = new LoginResquestDTO
			{
				Username = "nonexistentuser",
				Password = "whatever123!",
				RememberMe=false
			};

			// Act
			var response = await _client.PostAsJsonAsync("auth/login", dto);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			var msg = await response.Content.ReadAsStringAsync();
			(msg.Contains("Invalid") || msg.Contains("not found")).Should().BeTrue();
		}
	}
}

