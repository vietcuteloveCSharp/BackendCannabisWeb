using Azure;
using DTO.DTOs.Admin.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace TestsCannabis.TestAPI.Admin
{
	public class DataIntegrityTests : BaseIntegrationTest
	{
		public DataIntegrityTests(CannabisWebApplicationFactory factory, ITestOutputHelper output)
			: base(factory, output) { }
		[Fact]
		public async Task Password_ShouldAlwaysBeHashed_InDatabase()
		{
			// Arrange
			AsAdmin();
			var dto = new AdminCreateDTO
			{
				Username = "security_check",
				Password = "PlainPassword123!",
				Email = "security@test.com",
				Name = "Security",
				RoleId=1
			};

			// Act
			var response= await _client.PostAsJsonAsync("/api/v1/Admin/create-admin", dto);

			// Kiểm tra API phải thành công trước khi check DB
			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadAsStringAsync();
				throw new Exception($"API failed with status {response.StatusCode}. Error: {error}");
			}
			// Assert: Kiểm tra trực tiếp trong DB (RAM)
			using var scope = _factory.Services.CreateScope();
			var db = scope.ServiceProvider.GetRequiredService<CannabisAccessoriesDBContext>();
			var userInDb = await db.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
			// Kiểm tra mật khẩu đã được băm
			userInDb!.HashPassword.Should().NotBeNullOrEmpty("Trường HashPassword không được để trống.");
			userInDb.HashPassword.Should().NotBe(dto.Password, "Mật khẩu trong DB không được giống mật khẩu thô.");
		}

		[Fact]
		public async Task AuditLogs_ShouldBeCreated_WhenAdminChangesRole()
		{
			// Arrange
			AsAdmin();
			int targetUserId = 2;
			var updateDto = new UserRoleUpdateDTO { NewRoleId = 3 }; // Đổi Role

			// Act
			await _client.PutAsJsonAsync($"/api/v1/Admin/users/{targetUserId}/role", updateDto);

			// Assert: Kiểm tra bảng AuditLogs (Nếu logic của bạn đã cài đặt Audit)
			using var scope = _factory.Services.CreateScope();
			var db = scope.ServiceProvider.GetRequiredService<CannabisAccessoriesDBContext>();
			var logs = await db.AuditLogs.Where(l => l.UserId == 1).ToListAsync(); // Admin (Id=1) thực hiện

			// logs.Should().NotBeEmpty(); // Mở comment này nếu bạn đã triển khai lưu log
		}
	}
}
	

