using System.Text.Json;
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
		[Theory(DisplayName = "POST auth/login - edge cases")]
		[InlineData("", "Vuvietanh1!", HttpStatusCode.BadRequest, "username")]
		[InlineData("admin", "", HttpStatusCode.BadRequest, "password")]
		[InlineData("admin", "wrong", HttpStatusCode.Unauthorized, "invalid")]
		[InlineData("ghost", "123456", HttpStatusCode.Unauthorized, "invalid")]
		public async Task Login_ShouldHandleEdgeCases(
			string username,
			string password,
			HttpStatusCode expectedStatus,
			string expectedMessagePart)
		{
			// Arrange
			var dto = new LoginResquestDTO
			{
				Username = username,
				Password = password,
				RememberMe =false
			};

			// Act
			var response = await _client.PostAsJsonAsync("auth/login", dto);
			var content = await response.Content.ReadAsStringAsync();

			// Assert
			response.StatusCode.Should().Be(expectedStatus, because: content);

			(content.Contains(expectedMessagePart, StringComparison.OrdinalIgnoreCase))
				.Should().BeTrue(because: $"response should mention '{expectedMessagePart}'");
		}
	}
}



