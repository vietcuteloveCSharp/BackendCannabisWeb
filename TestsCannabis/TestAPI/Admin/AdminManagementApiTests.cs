using DTO.DTOs.Admin.Admins;
using DTO.DTOs.ChipModels;
using DTO.Response;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TestsCannabis.Mocks;
using Xunit.Abstractions;
using System.IdentityModel.Tokens.Jwt;

namespace TestsCannabis.TestAPI.Admin
{
	public class AdminManagementApiTests :BaseIntegrationTest
	{
		public AdminManagementApiTests(CannabisWebApplicationFactory factory, ITestOutputHelper output) : base(factory, output) { }
		[Fact]
		// --- NHÓM 1: PHÂN QUYỀN (SECURITY) ---

		public async Task CreateAdmin_ShouldReturnForbidden_WhenUserIsNormalUser()
		{
			// Arrange
			AsUser(); // Giả lập User thường
			var dto = CreateValidAdminDto();

			// Act
			var response = await _client.PostAsJsonAsync("/api/v1.0/Admin/create-admin", dto);

			// 1. Xem mã trạng thái
			Console.WriteLine($"Status: {response.StatusCode}");

			// 2. Xem URL trả về trong Header (Nếu là 201 Created)
			if (response.Headers.Location != null)
			{
				Console.WriteLine($"Location URL: {response.Headers.Location}");
			}

			// 3. Nếu là lỗi 400, in toàn bộ nội dung ra
			var content = await response.Content.ReadAsStringAsync();
			Console.WriteLine($"Response Content: {content}");
			// Assert
			// Nếu vẫn ra 400, hãy đọc nội dung lỗi bên dưới
			if (response.StatusCode == HttpStatusCode.BadRequest)
			{
				var error = await response.Content.ReadAsStringAsync();
				throw new Exception($"Nội dung lỗi thực tế: {error}");
			}

			response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
		}

		[Fact]
		public async Task CreateAdmin_ShouldReturnUnauthorized_WhenNoAuthHeader()
		{
			// Arrange
			_client.DefaultRequestHeaders.Authorization = null; // Không có token
			var dto = CreateValidAdminDto();

			// Act
			var response = await _client.PostAsJsonAsync("/api/v1/Admin/create-admin", dto);
			var content = await response.Content.ReadAsStringAsync();
			// In ra cửa sổ Output để xem message lỗi là gì
			_output.WriteLine($"Nội dung lỗi thực tế: {content}");
			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
		}

		// --- NHÓM 2: VALIDATION (DỮ LIỆU ĐẦU VÀO) ---

		[Theory]
		[InlineData("", "valid@email.com", "Password123!")] // Trống Username
		[InlineData("admin", "invalid-email", "Password123!")] // Sai định dạng Email
		[InlineData("admin", "valid@email.com", "123")]       // Mật khẩu quá yếu
		public async Task CreateAdmin_ShouldReturnBadRequest_WhenInputIsInvalid(string user, string email, string pass)
		{
			// Arrange
			AsAdmin();
			var invalidDto = new AdminCreateDTO
			{
				Username = user,
				Email = email,
				Password = pass,
				Name = "Test"
			};

			// Act
			var response = await _client.PostAsJsonAsync("/api/v1/Admin/create-admin", invalidDto);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
		}

		// --- NHÓM 3: LOGIC NGHIỆP VỤ & DATABASE (DEEP TEST) ---

		[Fact]
		public async Task CreateAdmin_ShouldSaveToDb_AndReturnCorrectInfo_WhenDataIsValid()
		{
			// 1. Arrange: Thiết lập quyền Admin cho Client
			AsAdmin();

			var uniqueSuffix = Guid.NewGuid().ToString().Substring(0, 4);
			var uniqueUser = $"admin_{uniqueSuffix}";

			// Lưu ý: RoleId = 1 tương ứng với ERoleName.Admin trong DbSeeder của bạn
			var dto = new AdminCreateDTO
			{
				Username = uniqueUser,
				Email = $"{uniqueUser}@cannabis.com",
				Password = "Vuvietanh1!",
				Name = "Chủ Tịch Admin",
				RoleId = 1,
			};

			// 2. Act: Gửi request tạo Admin
			var response = await _client.PostAsJsonAsync("/api/v1/Admin/create-admin", dto);

			// 3. Debug: Nếu lỗi 400, in chi tiết để soi Validation hoặc Enum error
			if (response.StatusCode == HttpStatusCode.BadRequest)
			{
				var errorJson = await response.Content.ReadAsStringAsync();
				// Bạn có thể xem dòng này trong cửa sổ 'Test Detail Summary' của xUnit
				Console.WriteLine($"[DEBUG ERROR 400]: {errorJson}");
			}

			// 4. Assert: Kiểm tra kết quả trả về
			// Kiểm tra Http StatusCode là 201 Created
			response.StatusCode.Should().Be(HttpStatusCode.Created);

			// Giải mã JSON sử dụng _jsonOptions (có chứa JsonStringEnumConverter)
			var body = await response.Content.ReadFromJsonAsync<ApiResponse<UserDTO>>(_jsonOptions);

			// Kiểm tra cấu hình ApiResponse chuẩn
			body.Should().NotBeNull();
			body!.Success.Should().BeTrue(); // Đổi Success -> Succeeded theo chuẩn ApiResponse của bạn

			// Kiểm tra dữ liệu thực tế
			body.Data.Should().NotBeNull();
			body.Data!.Username.Should().Be(uniqueUser);
			body.Data.Email.Should().Be(dto.Email);

		}
		[Fact]
		public void CheckConfig_Test()
		{
			// Lấy config từ Server ảo đang chạy
			var config = _factory.Services.GetRequiredService<IConfiguration>();

			var key = config["Jwt:Key"];
			var issuer = config["Jwt:Issuer"];
			var audience = config["Jwt:Audience"];

			// In ra cửa sổ Test Output
			_output.WriteLine($"DEBUG - Key từ Config: {key}");
			_output.WriteLine($"DEBUG - Issuer từ Config: {issuer}");
			_output.WriteLine($"DEBUG - Audience từ Config: {audience}");

			// Nếu các giá trị này null, bài Test sẽ fail tại đây
			key.Should().NotBeNullOrEmpty();
		}
		[Fact]
		public async Task CreateAdmin_ShouldFail_WhenUsernameAlreadyExists()
		{
			// Arrange
			AsAdmin();
			// testadmin01 đã có trong DbSeeder.cs
			var duplicateDto = new AdminCreateDTO
			{
				Username = "testadmin01",
				Email = "newemail@example.com",
				Password = "Vuvietanh1!",
				Name = "Trùng Tên"
				
			};

			// Act
			var response = await _client.PostAsJsonAsync("/api/v1/Admin/create-admin", duplicateDto);

			// Assert
			// Tùy vào Logic Service của bạn trả về lỗi gì (thường là BadRequest hoặc Conflict)
			response.StatusCode.Should().Match(s => s == HttpStatusCode.BadRequest || s == HttpStatusCode.Conflict);
		}
		#region 1. TEST PHÂN TRANG & BỘ LỌC (PAGING & FILTERING)

		[Fact]
		public async Task GetUsers_ShouldReturnPagedData_AndCorrectTotalCount()
		{
			// Arrange
			AsAdmin();
			var pageSize = 5;
			var url = $"/api/v1/Admin/users?PageNumber=1&PageSize={pageSize}";

			// Act
			var response = await _client.GetAsync(url);
			var result = await GetContentAsync<ApiResponse<PagedResult<UserDTO>>>(response);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			result!.Success.Should().BeTrue();
			result.Data.Items.Count().Should().BeLessThanOrEqualTo(pageSize);
			result.Data.TotalItems.Should().BeGreaterThan(0); // Vì đã có Seed data

			// Kiểm tra xem RoleName có được Map từ Enum sang String không
			result.Data.Items.Should().AllSatisfy(u => u.RoleName.Should().NotBeNullOrEmpty());
		}

		[Fact]
		public async Task GetUsers_SearchFilter_ShouldReturnFilteredResults()
		{
			// Arrange
			AsAdmin();
			var searchTerm = "testadmin01"; // User này có trong DbSeeder
			var url = $"/api/v1/Admin/users?SearchTerm={searchTerm}";

			// Act
			var response = await _client.GetAsync(url);
			var result = await GetContentAsync<ApiResponse<PagedResult<UserDTO>>>(response);

			// Assert
			result!.Data.Items.Should().Contain(u => u.Username.Contains(searchTerm));
		}

		#endregion

		#region 2. TEST CẬP NHẬT TRẠNG THÁI (BLOCK USER)

		[Fact]
		public async Task UpdateUserStatus_ToBlocked_ShouldReflectInDatabase()
		{
			// Arrange
			AsAdmin();
			int targetUserId = 2; // Giả sử Id của testuser01
			var updateDto = new UserStatusUpdateDTO { Status = EUserStatus.Inactive };

			// Act: Gọi API cập nhật trạng thái
			var response = await _client.PatchAsJsonAsync($"/api/v1/Admin/users/{targetUserId}/status", updateDto);

			// Assert API Response
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			// Assert DEEP: Kiểm tra trực tiếp trong DB (RAM)
			using var scope = _factory.Services.CreateScope();
			var db = scope.ServiceProvider.GetRequiredService<CannabisAccessoriesDBContext>();
			var userInDb = await db.Users.FindAsync(targetUserId);

			userInDb!.Status.Should().Be(EUserStatus.Inactive);
		}

		#endregion

		#region 3. TEST PHÂN QUYỀN CHÉO (SECURITY DEEP TEST)

		[Fact]
		public async Task ChangeUserRole_ShouldReturnForbidden_WhenCalledByUser()
		{
			// Arrange: Đăng nhập bằng tài khoản User thường
			AsUser();
			var updateDto = new UserRoleUpdateDTO { NewRoleId = 1 }; // Thử tự nâng cấp lên Admin

			// Act
			var response = await _client.PutAsJsonAsync("/api/v1/Admin/users/2/role", updateDto);

			// Assert: Phải bị chặn ở tầng Middleware/Controller
			response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
		}

		[Fact]
		public async Task ChangeUserRole_ValidAdmin_ShouldUpdateSuccessfully()
		{
			// Arrange
			AsAdmin();
			int targetUserId = 2;
			var updateDto = new UserRoleUpdateDTO { NewRoleId = 3 }; // Đổi từ User sang Employee

			// Act
			var response = await _client.PutAsJsonAsync($"/api/v1/Admin/users/{targetUserId}/role", updateDto);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			// Kiểm tra DB xem RoleId đã đổi chưa
			using var scope = _factory.Services.CreateScope();
			var db = scope.ServiceProvider.GetRequiredService<CannabisAccessoriesDBContext>();
			var userInDb = await db.Users.FindAsync(targetUserId);
			userInDb!.RoleId.Should().Be(3);
		}

        #endregion


		protected void InspectCurrentToken(string token)
{
			var handler = new JwtSecurityTokenHandler();
    
			if (handler.CanReadToken(token))
			{
				var jwtToken = handler.ReadJwtToken(token);
        
				// In ra Console để xem trong Output của Test
				 Console.WriteLine("===== [TOKEN INSPECTION] =====");
				Console.WriteLine($"Subject (sub): {jwtToken.Subject}");
				Console.WriteLine($"Issuer (iss): {jwtToken.Issuer}");
				Console.WriteLine($"Audience (aud): {string.Join(", ", jwtToken.Audiences)}");
        
				Console.WriteLine("--- Claims ---");
				foreach (var claim in jwtToken.Claims)
				{
					 Console.WriteLine($"- {claim.Type}: {claim.Value}");
					}
				Console.WriteLine("==============================");
			 }
			else
			{
					Console.WriteLine("!!! Lỗi: Chuỗi không phải là JWT hợp lệ.");
			}
		}
		// --- PRIVATE HELPER ---
		private AdminCreateDTO CreateValidAdminDto() => new AdminCreateDTO
		{
			Username = "test_admin_" + Guid.NewGuid().ToString().Substring(0, 5),
			Password = "Vuvietanh1!", // Thỏa mãn Regex: 8 ký tự, 1 chữ hoa
			Email = "test@example.com",
			Name = "Nguyễn Văn Test",
			RoleId = 1 // Admin
		};
	}
}

