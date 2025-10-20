namespace TestsCannabis.TestAPI.AuthControllerTests
{
	public class TestSendOtp_Logic :IClassFixture<CannabisWebApplicationFactory> 
	{
		private readonly HttpClient _client;
		public TestSendOtp_Logic(CannabisWebApplicationFactory factory)
		{
			_client = factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				BaseAddress = new Uri("https://localhost/api/v1/")
			});
		}
		[Fact(DisplayName = "POST auth/send-otp - 200 when email exists")]
		public async Task SendOtp_ShouldReturn200_WhenEmailExists()
		{
			// Arrange: dùng email có sẵn trong DB seed
			var existingEmail = "testadmin01@test.com";

			// Act
			var response = await _client.PostAsync($"auth/send-otp?email={existingEmail}", null);
			var content = await response.Content.ReadAsStringAsync();

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK, because: content);
			content.Should().ContainEquivalentOf("otp sent successfully",
				because: "OTP should be sent to an existing user");
		}
	}
}
