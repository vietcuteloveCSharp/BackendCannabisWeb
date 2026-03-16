using DTO.DTOs.Admin.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace TestsCannabis.TestAPI.Admin
{
	public class AdminTests_Login  :IClassFixture<CannabisWebApplicationFactory>
	{
		private readonly HttpClient _client;
		private readonly HttpClient _adminClient;
		private readonly HttpClient _employeeClient;
		private readonly HttpClient _userClient;
		private readonly FakeAuthFactory_Admin _adminFactory;
		private readonly FakeAuthFactory_Employee _employeeFactory;
		private readonly FakeAuthFactory_User _userFactory;
		private readonly ITestOutputHelper _output;
		public AdminTests_Login(CannabisWebApplicationFactory factory, ITestOutputHelper output)
		{
			_output = output;
			_client = factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				BaseAddress = new Uri("https://localhost/api/v1/")
			});
			_adminFactory = new FakeAuthFactory_Admin();
			_employeeFactory = new FakeAuthFactory_Employee();
			_userFactory = new FakeAuthFactory_User();

			_adminClient = _adminFactory.CreateClient(new WebApplicationFactoryClientOptions
			{
				BaseAddress = new Uri("https://localhost/api/v1/")
			});
			_employeeClient = _employeeFactory.CreateClient(new WebApplicationFactoryClientOptions
			{
				BaseAddress = new Uri("https://localhost/api/v1/")
			});
			_userClient = _userFactory.CreateClient(new WebApplicationFactoryClientOptions
			{
				BaseAddress = new Uri("https://localhost/api/v1/")
			});
		}
		private void Dispose()
		{
			_client.Dispose();
			_adminClient.Dispose();
			_employeeClient.Dispose();
			_userClient.Dispose();

			_adminFactory.Dispose();
			_employeeFactory.Dispose();
			_userFactory.Dispose();
		}
		[Fact]
		public async Task RegisterAdmin_ShouldReturn201_WhenRoleIsAdmin()
		{
			var dto = new AdminCreateDTO
			{
				Username = "admin_create_test",
				Password = "StrongPassA1!",
				Name = "Admin Tester",
				Email = "admin.create@test.com",
				RoleId = 1
			};

			var response = await _adminClient.PostAsJsonAsync("Admin/register-admin", dto);
			_output.WriteLine($"Response body : {await response.Content.ReadAsStringAsync()}");
			response.StatusCode.Should().Be(HttpStatusCode.Created);
		}

		[Fact]
		public async Task RegisterAdmin_ShouldReturn403_WhenRoleIsEmployee()
		{
			var dto = new AdminCreateDTO
			{
				Username = "employee_try_admin",
				Password = "StrongPassA1!",
				Name = "Emp Tester",
				Email = "emp@test.com",
				RoleId = 1
			};

			var response = await _employeeClient.PostAsJsonAsync("Admin/register-admin", dto);

			response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
		}

		[Fact]
		public async Task RegisterAdmin_ShouldReturn403_WhenRoleIsUser()
		{
			var dto = new AdminCreateDTO
			{
				Username = "user_try_admin",
				Password = "StrongPassA1",
				Name = "User Tester",
				Email = "user@test.com",
				RoleId = 1
			};

			var response = await _userClient.PostAsJsonAsync("Admin/register-admin", dto);

			response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
		}
		[Fact]
		public async Task RegisterAdmin_ShouldReturn401_WhenNoToken()
		{
			// Arrange
			var dto = new AdminCreateDTO
			{
				Username = "unauth_test_user",
				Password = "StrongPassA1!",
				Name = "Unauth User",
				Email = "unauth@test.com",
				RoleId = 1
			};

			// Act
			var response = await _client.PostAsJsonAsync("Admin/register-admin", dto);
			var content = await response.Content.ReadAsStringAsync();
			_output.WriteLine($"Response: {response.StatusCode}, Body: {content}");
			// Check header của request
			foreach (var header in _client.DefaultRequestHeaders)
				_output.WriteLine($"{header.Key}: {string.Join(",", header.Value)}");
			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
			
		}
	}
}

