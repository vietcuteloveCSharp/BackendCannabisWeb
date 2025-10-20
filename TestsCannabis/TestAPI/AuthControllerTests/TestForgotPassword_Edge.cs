
using TestsCannabis.Mocks;

namespace TestsCannabis.TestAPI.AuthControllerTests
{
	public class TestForgotPassword_Edge :IClassFixture<CannabisWebApplicationFactory>
	{
		private readonly HttpClient _client;
		private readonly FakeRedisService _fakeRedis;

		public TestForgotPassword_Edge(CannabisWebApplicationFactory factory)
		{
			_client = factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				BaseAddress = new Uri("https://localhost/api/v1/")
			});

			_fakeRedis = factory.Services.GetRequiredService<FakeRedisService>();
		}

		[Fact(DisplayName = "POST auth/forgot-password - 200 when OTP valid and password reset successfully")]
		public async Task ForgotPassword_ShouldReturn200_WhenOtpValid()
		{
			// Arrange
			var email = "admin01@example.com";
			var otp = "123456";
			_fakeRedis.SeedOtp(email, otp);

			var dto = new ResetPasswordParam
			{
				Email = email,
				Otp = otp,
				NewPassword = "NewStrongPassA1!"
			};

			// Act
			var response = await _client.PostAsJsonAsync("auth/forgot-password", dto);
			var content = await response.Content.ReadAsStringAsync();

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK, because: content);
			content.Should().ContainEquivalentOf("password reset successfully");
		}
	}
}

