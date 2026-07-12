using ChangePasswordRequest = Shared.DTOs.DTO.Shop.ChangePasswordRequest;
using RegisterRequest = Shared.DTOs.DTO.Shop.RegisterRequest;
namespace Service.Implementations.Shop
{
	public class CustomerService :ICustomerService
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IPasswordHasher<Customer> _passwordHasher;

		public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher<Customer> passwordHasher)
		{
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
		}

		/// <summary>
		/// Đăng ký tài khoản khách hàng mua sắm ngoài giao diện cửa hàng
		/// </summary>
		public async Task<CustomerDTO> RegisterCustomerAsync(RegisterRequest request)
		{
			ArgumentNullException.ThrowIfNull(request);

			// 1. Kiểm tra trùng lặp dữ liệu dưới DB tầng Shop
			if (await _unitOfWork.Customers.EmailExistsAsync(request.Email))
			{
				throw new InvalidOperationException("Email đã được sử dụng trên hệ thống.");
			}

			if (await _unitOfWork.Customers.UserNameExistsAsync(request.Username))
			{
				throw new InvalidOperationException("Tên đăng nhập đã tồn tại.");
			}

			// 2. Ánh xạ DTO sang Entity
			var customerEntity = _mapper.Map<Customer>(request);

			// 3. Mã hóa mật khẩu bảo mật
			customerEntity.PasswordHash = _passwordHasher.HashPassword(customerEntity, request.Password);
			customerEntity.IsActive = true; // Trạng thái nhanh mặc định hoạt động

			// 4. Lưu xuống Database
			var result = await _unitOfWork.Customers.AddAsync(customerEntity);
			await _unitOfWork.SaveChangesAsync();

			return _mapper.Map<CustomerDTO>(result);
		}

		/// <summary>
		/// Lấy thông tin hồ sơ cá nhân của khách hàng
		/// </summary>
		public async Task<CustomerDTO> GetProfileAsync(int customerId)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(customerId, nameof(customerId));

			var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
			if (customer == null || customer.IsDeleted)
			{
				throw new KeyNotFoundException($"Không tìm thấy tài khoản khách hàng với ID {customerId}.");
			}

			return _mapper.Map<CustomerDTO>(customer);
		}

		/// <summary>
		/// Cập nhật thông tin cá nhân (Họ tên, Số điện thoại, Avatar...) của khách hàng
		/// </summary>
		public async Task<CustomerDTO> UpdateProfileAsync(int customerId, UpdateRequest request)
		{
			ArgumentNullException.ThrowIfNull(request);

			var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
			if (customer == null || customer.IsDeleted)
			{
				throw new KeyNotFoundException($"Không tìm thấy khách hàng cần cập nhật với ID {customerId}.");
			}

			// Thực hiện đè dữ liệu thay đổi từ DTO sang Entity thông qua cấu hình AutoMapper
			_mapper.Map(request, customer);
			customer.UpdatedAt = DateTime.UtcNow;

			var updated = _unitOfWork.Customers.Update(customer);
			await _unitOfWork.SaveChangesAsync();

			return _mapper.Map<CustomerDTO>(updated);
		}

		/// <summary>
		/// Khách hàng tự thay đổi mật khẩu cá nhân
		/// </summary>
		public async Task<bool> ChangePasswordAsync(int customerId,ChangePasswordRequest request)
		{
			ArgumentNullException.ThrowIfNull(request);

			var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
			if (customer == null || customer.IsDeleted)
			{
				throw new KeyNotFoundException("Tài khoản khách hàng không tồn tại.");
			}

			// 1. Kiểm tra tính chính xác của mật khẩu cũ
			var verifyOldPass = _passwordHasher.VerifyHashedPassword(customer, customer.PasswordHash!, request.OldPassword);
			if (verifyOldPass == PasswordVerificationResult.Failed)
			{
				throw new InvalidOperationException("Mật khẩu cũ không chính xác.");
			}

			// 2. Hash mật khẩu mới và cập nhật trạng thái thời gian đổi
			customer.PasswordHash = _passwordHasher.HashPassword(customer, request.NewPassword);
			customer.UpdatedAt = DateTime.UtcNow;

			_unitOfWork.Customers.Update(customer);
			return await _unitOfWork.SaveChangesAsync() > 0;
		}
	}
}
