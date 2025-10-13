
namespace TestsCannabis.TestAPI.AuthControllerTests
{
	public class TestRegister_Logic : IClassFixture<CannabisWebApplicationFactory>
	{
		private readonly HttpClient _client;

		public TestRegister_Logic(CannabisWebApplicationFactory factory)
		{
			_client = factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				BaseAddress = new Uri("https://localhost/api/v1/")
			});
		}

		[Fact(DisplayName = "Register - Should return 201 Created when valid data provided")]
		public async Task Register_ShouldReturnCreated_WhenValidData()
		{
			// Arrange
			var payload = new CreateUserDTO
			{
				Email = "newuser01@example.com",
				Username = "newuser01",
				Password = "Abc@12345"
			};

			// Act
			var response = await _client.PostAsJsonAsync("auth/register", payload);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.Created);

			var user = await response.Content.ReadFromJsonAsync<UserDTO>();
			user.Should().NotBeNull();
			user!.Email.Should().Be(payload.Email);
			user.Username.Should().Be(payload.Username);
		}

		[Fact(DisplayName = "Register - Should allow multiple unique users")]
		public async Task Register_ShouldAllow_MultipleUniqueUsers()
		{
			// Arrange
			var firstUser = new CreateUserDTO
			{
				Email = "uniqueuser1@example.com",
				Username = "uniqueuser1",
				Password = "StrongPass1!"
			};

			var secondUser = new CreateUserDTO
			{
				Email = "uniqueuser2@example.com",
				Username = "uniqueuser2",
				Password = "StrongPass2!"
			};

			// Act
			var firstResponse = await _client.PostAsJsonAsync("auth/register", firstUser);
			var secondResponse = await _client.PostAsJsonAsync("auth/register", secondUser);

			// Assert
			firstResponse.StatusCode.Should().Be(HttpStatusCode.Created);
			secondResponse.StatusCode.Should().Be(HttpStatusCode.Created);

			var firstUserRes = await firstResponse.Content.ReadFromJsonAsync<UserDTO>();
			var secondUserRes = await secondResponse.Content.ReadFromJsonAsync<UserDTO>();

			firstUserRes!.Email.Should().Be(firstUser.Email);
			secondUserRes!.Email.Should().Be(secondUser.Email);
			secondUserRes!.UserId.Should().NotBe(firstUserRes.UserId);
		}
	}
}

