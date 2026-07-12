
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit.Abstractions;

//namespace TestsCannabis.TestAPI.Admin
//{
//	public class AuthSecurityTests : BaseIntegrationTest
//	{
//		public AuthSecurityTests(CannabisWebApplicationFactory factory, ITestOutputHelper output)
//			: base(factory, output) { }
//		[Fact]
//		public async Task Access_ShouldBeRevoked_Immediately_WhenAdminBlocksUser()
//		{
//			// 1. User đăng nhập (Token OK)
//			AsUser();

//			// 2. Admin khóa User trực tiếp trong DB
//			using (var scope = _factory.Services.CreateScope())
//			{
//				var db = scope.ServiceProvider.GetRequiredService<CannabisAccessoriesDBContext>();
//				var user = await db.Users.FirstOrDefaultAsync(u => u.Id == 2);
//				await db.SaveChangesAsync();
//			}

//			// 3. Gọi API cần [Authorize]
//			var secondCheck = await _client.GetAsync("/api/v1/User/2");

//			// Assert: Kỳ vọng là 401 Unauthorized vì User đã bị Inactive
//			secondCheck.StatusCode.Should().Be(HttpStatusCode.Unauthorized,
//				"User phải bị chặn ngay lập tức khi trạng thái chuyển sang Inactive.");
//		}

//		[Fact]
//		public async Task Logout_ShouldInvalidateRefreshToken_InDatabase()
//		{
//			// Arrange: Login để có Refresh Token trong Cookie
//			var loginDto = new LoginResquestDTO { Username = "testuser01", Password = "Vuvietanh1!" };
//			var loginRes = await _client.PostAsJsonAsync("/api/v1/Auth/login", loginDto);

//			// Act: Gọi Logout
//			var logoutRes = await _client.PostAsJsonAsync("/api/v1/Auth/logout", new LogoutRequestDTO());

//			// Assert: Kiểm tra DB xem Refresh Token đã bị xóa chưa
//			using var scope = _factory.Services.CreateScope();
//			var db = scope.ServiceProvider.GetRequiredService<CannabisAccessoriesDBContext>();
//			var tokens = await db.RefreshTokens.Where(t => t.UserId == 2).ToListAsync();
//			tokens.Should().BeEmpty(); // Đã bị xóa hoàn toàn khỏi DB
//		}
//	}
//}
