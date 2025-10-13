namespace TestsCannabis.TestAPI.AuthControllerTests
{
	public class TestRegister_EdgeCase : IClassFixture<CannabisWebApplicationFactory>
	{
		private readonly HttpClient _client;

		public TestRegister_EdgeCase(CannabisWebApplicationFactory factory)
		{
			_client = factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				BaseAddress = new Uri("https://localhost/api/v1/")
			});
		}

		[Fact(DisplayName = "Register - Should return 400 when model invalid (missing fields)")]
		public async Task Register_ShouldReturnBadRequest_WhenModelInvalid()
		{
			// Arrange — thiếu toàn bộ field
			var payload = new CreateUserDTO
			{
				Email = "",
				Username = "",
				Password = ""
			};

			// Act
			var response = await _client.PostAsJsonAsync("auth/register", payload);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			var raw = await response.Content.ReadAsStringAsync();
			raw.Should().Contain("Email");  // gợi ý lỗi model binding
		}

		[Fact(DisplayName = "Register - Should return 400 when email already exists")]
		public async Task Register_ShouldReturnBadRequest_WhenEmailExists()
		{
			// Arrange — email đã seed trong DB
			var payload = new CreateUserDTO
			{
				Email = "admin01@example.com",
				Username = "duplicatedUser",
				Password = "Test@1234"
			};

			// Act
			var response = await _client.PostAsJsonAsync("auth/register", payload);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			var body = await response.Content.ReadAsStringAsync();
			body.Should().Contain("Email");
		}

		[Fact(DisplayName = "Register - Should return 400 when password too short")]
		public async Task Register_ShouldReturnBadRequest_WhenPasswordTooShort()
		{
			// Arrange — password ngắn
			var payload = new CreateUserDTO
			{
				Email = "shortpass@example.com",
				Username = "shortpass",
				Password = "12"
			};

			// Act
			var response = await _client.PostAsJsonAsync("auth/register", payload);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			var resultText = await response.Content.ReadAsStringAsync();
			resultText.Should().Contain("Password");
		}
	}
}

