namespace ApiCannabisServer.TestService
{
	public class UserServiceTests
	{
		private readonly Mock<IUnitOfWork> _unitOfWorkMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly UserService _service;
		public UserServiceTests()
		{
			_unitOfWorkMock = new Mock<IUnitOfWork>();
			_mapperMock = new Mock<IMapper>();
			_service = new UserService(_unitOfWorkMock.Object, _mapperMock.Object);
		}
		[Fact]
		public async Task GetUserByIdAsync_IdIsZero_ThrowsArgumentOutOfRangeException()
		{
			// Arrange
			var userId = 0;

			// Act & Assert
			var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.GetUserByIdAsync(userId));

			Assert.Equal("id", exception.ParamName);
		}
		[Fact]
		public async Task GetUserByIdAsync_UserNotFound_ThrowsNotFoundException()
		{
			// Arrange
			var userId = 99;
			_unitOfWorkMock.Setup(repo => repo.Users.GetByIdAsync(userId)).ReturnsAsync((User)null!);

			// Act & Assert
			var exception = await Assert.ThrowsAsync<NotFoundException>(() => _service.GetUserByIdAsync(userId));

			Assert.Equal($"User with ID {userId} not found.", exception.Message);
		}
		[Fact]
		public async Task GetUserByIdAsync_ValidId_ReturnsMappedUserDTO()
		{
			// Arrange
			var userId = 1;
			var user = new User { UserId = userId, Name = "Test User" };
			var expectedDto = new UserDTO { UserId = userId, Name = "Test User" };
			_unitOfWorkMock.Setup(repo => repo.Users.GetByIdAsync(userId)).ReturnsAsync(user);
			_mapperMock.Setup(mapper => mapper.Map<UserDTO>(user)).Returns(expectedDto);

			// Act
			var result = await _service.GetUserByIdAsync(userId);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(expectedDto.UserId, result.UserId);
			Assert.Equal(expectedDto.Name, result.Name);
		}
	}
}
