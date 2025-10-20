namespace TestsCannabis.TestAPI.AuthControllerTests
{
	public class TestSendOtp_Edge :IClassFixture<CannabisWebApplicationFactory>
	{
		private readonly HttpClient _client;
		public TestSendOtp_Edge(CannabisWebApplicationFactory factory)
		{
			_client = factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				BaseAddress = new Uri("https://localhost/api/v1/")
			});
		}
		[Theory(DisplayName = "POST auth/send-otp - 400 when email missing or empty")]
		[InlineData("")]
		[InlineData("   ")]
		[InlineData(null)]
		public async Task SendOtp_ShouldReturn400_WhenEmailMissingOrEmpty(string? email)
		{
			// Arrange
			var url = email is null ? "auth/send-otp" : $"auth/send-otp?email={email}";

			// Act
			var response = await _client.PostAsync(url, null);
			var content = await response.Content.ReadAsStringAsync();

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest, because: content);
			content.Should().ContainEquivalentOf("required",
				because: "email parameter is required by model binding");
		}

		[Fact(DisplayName = "POST auth/send-otp - 404 when email not found")]
		public async Task SendOtp_ShouldReturn404_WhenEmailNotFound()
		{
			// Arrange
			var nonExistingEmail = "ghost@test.com";

			// Act
			var response = await _client.PostAsync($"auth/send-otp?email={nonExistingEmail}", null);
			var content = await response.Content.ReadAsStringAsync();
			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.NotFound, because: content);

			var lower = content.ToLowerInvariant();
			(lower.Contains("not found") || lower.Contains("invalid"))
				.Should().BeTrue(because: "API should indicate the email does not exist");
		}
	}
}
