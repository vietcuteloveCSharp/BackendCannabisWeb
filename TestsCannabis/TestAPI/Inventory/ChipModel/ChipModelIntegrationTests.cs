using DTO.DTOs.ChipModels;
using DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace TestsCannabis.TestAPI.Inventory.ChipModel
{
	public class ChipModelIntegrationTests : BaseIntegrationTest
	{
		private readonly string _baseUrl = "/api/v1/ChipModel";

		public ChipModelIntegrationTests(CannabisWebApplicationFactory factory, ITestOutputHelper output)
			: base(factory, output)
		{
		}
		[Fact]
		public async Task GetAllAsync_ShouldReturnAllSeedData()
		{
			// Arrange
			AsAdmin(); // ChipModelController yêu cầu Role Admin

			// Act
			var response = await _client.GetAsync(_baseUrl);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			var result = await GetContentAsync<ApiResponse<IEnumerable<ChipModelDTO>>>(response);

			result!.Success.Should().BeTrue();
			result.Data.Should().NotBeNull();
			result.Data.Should().HaveCountGreaterThanOrEqualTo(5); // Vì DbSeeder nạp 5 chip
			result.Data!.Any(x => x.ModelChip == "LM301H").Should().BeTrue();
		}

		[Fact]
		public async Task GetByIdAsync_ShouldReturnCorrectChip_WhenIdExists()
		{
			// Arrange
			AsAdmin();
			int existingId = 1; // Chip Samsung LM301H trong DbSeeder

			// Act
			var response = await _client.GetAsync($"{_baseUrl}/{existingId}");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			var result = await GetContentAsync<ApiResponse<ChipModelDTO>>(response);

			result!.Success.Should().BeTrue();
			result.Data!.ChipModelId.Should().Be(existingId);
			result.Data.Manufacturer.Should().Be("Samsung");
			result.Data.ModelChip.Should().Be("LM301H");
		}

		[Fact]
		public async Task CreateAsync_ShouldReturnCreated_WhenDataIsValid()
		{
			// Arrange
			AsAdmin();
			var newChip = new ChipModelCreateDTO
			{
				Manufacturer = "Bridgelux",
				ModelChip = "Vero 29",
				Generation = "Gen 7",
				Efficiency = 2.5m
			};

			// Act
			var response = await _client.PostAsJsonAsync(_baseUrl, newChip);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.Created); // Trả về 201 CreatedAtAction
			var result = await GetContentAsync<ApiResponse<ChipModelDTO>>(response);

			result!.Success.Should().BeTrue();
			result.Data!.ModelChip.Should().Be("Vero 29");
			response.Headers.Location.Should().NotBeNull(); // Kiểm tra header Location của CreatedAtAction
		}

		[Fact]
		public async Task UpdateAsync_ShouldReturnOk_WhenIdExists()
		{
			// Arrange
			AsAdmin();
			int idToUpdate = 2; // Samsung LM301B
			var updateDto = new ChipModelUpdateDTO
			{
				Manufacturer = "Samsung Updated",
				ModelChip = "LM301B-Pro",
				Efficiency = 3.0m
			};

			// Act
			var response = await _client.PutAsJsonAsync($"{_baseUrl}/{idToUpdate}", updateDto);

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			var result = await GetContentAsync<ApiResponse<bool>>(response);
			result!.Success.Should().BeTrue();
			result.Message.Should().Contain("Updated successfully");
		}

		[Fact]
		public async Task Delete_ShouldReturnOk_AndSetIsDeletedTrue()
		{
			// Arrange
			AsAdmin();
			int idToDelete = 4; // Cree XP-G3

			// Act
			var response = await _client.DeleteAsync($"{_baseUrl}/{idToDelete}");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			// Kiểm tra lại xem còn lấy ra được không (GetById trong Service của bạn check !IsDeleted)
			var getResponse = await _client.GetAsync($"{_baseUrl}/{idToDelete}");
			getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
		}

		[Fact]
		public async Task GetById_ShouldReturn401_WhenNotAuthenticated()
		{
			// Arrange: Không gọi AsAdmin(), xóa header Auth
			_client.DefaultRequestHeaders.Authorization = null;

			// Act
			var response = await _client.GetAsync($"{_baseUrl}/1");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
		}
	}
}
