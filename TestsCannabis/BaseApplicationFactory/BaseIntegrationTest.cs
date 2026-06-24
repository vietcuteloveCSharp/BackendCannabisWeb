
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TestsCannabis.Mocks;
using Xunit.Abstractions;

namespace TestsCannabis.BaseApplicationFactory
{
	public class BaseIntegrationTest :IClassFixture<CannabisWebApplicationFactory>
	{
		protected readonly HttpClient _client;
		protected readonly CannabisWebApplicationFactory _factory;
		protected readonly ITestOutputHelper _output;
		protected readonly JwtConfig _options;
		// cấu hình đọc json
		protected static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			// Giúp giải mã các Enum như ERoleName, EUserStatus từ String/Int sang Enum Object
			Converters = { new JsonStringEnumConverter() }
		};
		public BaseIntegrationTest(CannabisWebApplicationFactory factory, ITestOutputHelper output )
		{
			_client = factory.CreateClient();
			_factory = factory;
			_output = output;
			using (var scope = factory.Services.CreateScope())
			{
				// Lấy IOptions<JwtConfig> đã được đăng ký trong Factory
				var options = scope.ServiceProvider.GetRequiredService<IOptions<JwtConfig>>();
				_options = options.Value;
			}
			// Mặc định dùng TestScheme để vượt qua middleware Auth
			_client.DefaultRequestHeaders.Authorization = null;
			

		}

		protected async Task<T?> GetContentAsync<T>(HttpResponseMessage response)
		{
			return await response.Content.ReadFromJsonAsync<T>(_jsonOptions);
		}
		// Xếp cấu hình Admin
		protected void AsAdmin()
		{	//tạo token
			var token = GenerateTestToken("1", "testadmin01", "Admin"); // Khớp ID 1
			_output.WriteLine("===== DEBUG TOKEN FOR ADMIN =====");
			_output.WriteLine($"Token: {token}");
		
					InspectCurrentToken(token);

			//// 2. Dọn dẹp Header cũ
			//_client.DefaultRequestHeaders.Authorization = null;

			// 3. Gán Token vào Header (Đây là cách chuẩn để vượt qua Middleware JwtBearer)
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			
		}
		

		// Xếp cấu hình User thường
		protected void AsUser()
		{
			// Id=2 phải khớp với SeedData của User thường
			var token = GenerateTestToken("2", "testuser01", "User");

			//_client.DefaultRequestHeaders.Clear();
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			_output.WriteLine($"[AUTH] Đã thiết lập quyền User (JWT). ID: 2");
		}
		protected string GenerateTestToken(string userId, string username, string role)
		{
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, userId),
				new Claim(JwtRegisteredClaimNames.UniqueName, username),
				 // Dùng chuỗi "role" thay vì ClaimTypes.Role
				new Claim("role", role),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			// LƯU Ý: Key này phải khớp 100% với Key trong appsettings.Development.json của bạn
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.UtcNow.Add(_options.AccessTokenTimeSpan);
			// Bảo vệ trường hợp lifetime âm hoặc bằng 0
			if (_options.AccessTokenTimeSpan < TimeSpan.Zero)
			{
				// Giúp token có hiệu lực hợp lệ trong 1 giây (để tránh IDX12401)
				expires = DateTime.UtcNow.AddSeconds(60);

			}
			// Tạo token
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = expires,
				Issuer = _options.Issuer,
				Audience = _options.Audience,
				SigningCredentials = creds,
				NotBefore = DateTime.UtcNow
				
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
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
				foreach (var claim in jwtToken.Claims)
				{
					Console.WriteLine($"Claim: {claim.Type} -> {claim.Value}");
				}
				Console.WriteLine("----------------------");
			}
			else
			{
				Console.WriteLine("Invalid Token Format!");
			}
		}
	}
}
