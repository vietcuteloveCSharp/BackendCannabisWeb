using DTO.DTOs.Admin.Admins;
using DTO.Response;
using DTO.TokenDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace TestsCannabis.TestAPI.Admin
{
	public class AdminE2EFlowTests : BaseIntegrationTest
	{
		public AdminE2EFlowTests(CannabisWebApplicationFactory factory, ITestOutputHelper output)
			: base(factory, output) { }

		[Fact]
		public async Task FullAdminLifecycle_Flow_Test()
		{
			// BƯỚC 1: Admin tổng tạo một Admin cấp dưới
			AsAdmin();
			var newAdminDto = new AdminCreateDTO
			{
				Username = "new_admin_manager",
				Email = "manager@cannabis.com",
				Password = "NewPassword123!",
				Name = "Manager",
				RoleId = 1
			};
			var createRes = await _client.PostAsJsonAsync("/api/v1/Admin/create-admin", newAdminDto);
			createRes.StatusCode.Should().Be(HttpStatusCode.Created);

			// BƯỚC 2: Admin mới dùng tài khoản vừa tạo để Login
			var loginDto = new LoginResquestDTO { Username = newAdminDto.Username, Password = newAdminDto.Password };
			var loginRes = await _client.PostAsJsonAsync("/api/v1/Auth/login", loginDto);
			var loginResult = await GetContentAsync<ApiResponse<TokenDTO>>(loginRes);

			// BƯỚC 3: Dùng Token của Admin mới để truy cập danh sách User
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult!.Data.AccessToken);
			var listRes = await _client.GetAsync("/api/v1/Admin/users");

			// Assert
			listRes.StatusCode.Should().Be(HttpStatusCode.OK);
			var listData = await GetContentAsync<ApiResponse<PagedResult<UserDTO>>>(listRes);
			listData!.Data!.Items.Should().NotBeEmpty();
		}
	}
}
