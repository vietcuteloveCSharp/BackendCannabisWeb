using AutoMapper.QueryableExtensions;
using DTO.DTOs.Seeds;
using Microsoft.EntityFrameworkCore;
using Service.IServices.Inventory;

namespace Service.Services.Inventory
{
	public class SeedService : ISeedService
	{
		private readonly IUnitOfWork  _unitOfWork;
		private readonly IMapper _mapper;
		public SeedService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		//// transaction  tạo 1 seed mới 
		public async Task<SeedDTO> CreateAsync(SeedCreateRequestDTO dto)
		{
			// Bắt đầu Transaction
			await _unitOfWork.BeginTransactionAsync();
			try
			{
				// BƯỚC 1: Dùng Mapper tạo Product từ Mega DTO
				var product = _mapper.Map<DAL.Entities.Product>(dto);

				await _unitOfWork.Products.AddAsync(product);
				await _unitOfWork.SaveChangesAsync(); // Lưu để lấy ProductId

				// BƯỚC 2: Dùng Mapper tạo Seed từ Mega DTO
				var seed = _mapper.Map<Seed>(dto);
				seed.ProductId = product.ProductId; // Gán ID liên kết

				await _unitOfWork.Seeds.AddAsync(seed);

				// BƯỚC 3: Chốt đơn
				await _unitOfWork.CommitTransactionAsync();

				return await GetByIdAsync(seed.SeedId) ?? _mapper.Map<SeedDTO>(seed);

			}
			catch (Exception)
			{

				await _unitOfWork.RollbackTransactionAsync();
				throw;
			}
		}
		//get all seed
		public async Task<IEnumerable<SeedDTO>> GetAllAsync()
		{
			return await _unitOfWork.Seeds.GetQueryable()
				.Where(s => !s.IsDeleted)
				.ProjectTo<SeedDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();
		}
		//get all seed active
		public async Task<IEnumerable<SeedDTO>> GetAllActiveAsync()
		{
			return await _unitOfWork.Seeds.GetQueryable()
				.Where(s => !s.IsDeleted)
				.ProjectTo<SeedDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();
		}
		//getbyid
		public async Task<SeedDTO?> GetByIdAsync(int id)
		{
			return await _unitOfWork.Seeds.GetQueryable()
				.Where(s => s.SeedId == id && !s.IsDeleted)
				.ProjectTo<SeedDTO>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync();

		}
		// transaction update seed
		public async Task<bool>  UpdateAsync(int id, SeedUpdateDTO dto)
		{
			await _unitOfWork.BeginTransactionAsync();
			try
			{
				// Kiểm tra Seed có tồn tại không
				var existingSeed = await _unitOfWork.Seeds.GetByIdAsync(id);
				if (existingSeed == null || existingSeed.IsDeleted)
					throw new KeyNotFoundException($"Không tìm thấy hạt giống với ID {id}");

				// Map dữ liệu từ DTO vào Entity hiện tại
				_mapper.Map(dto, existingSeed);
				existingSeed.UpdatedAt = DateTime.Now;

				_unitOfWork.Seeds.Update(existingSeed);

				// NẾU BẠN MUỐN: Cập nhật luôn giá/số lượng ở bảng Product cha (nếu có)
				// var product = await _unitOfWork.Products.GetByIdAsync(existingSeed.ProductId);
				// if (product != null) { product.Price = dto.Price; _unitOfWork.Products.Update(product); }

				await _unitOfWork.CommitTransactionAsync();
				return true;
			}
			catch (Exception)
			{
				await _unitOfWork.RollbackTransactionAsync();
				throw;
			}
		}
		// transaction delete
		public async Task<bool> DeleteAsync(int id)
		{
			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var seed = await _unitOfWork.Seeds.GetByIdAsync(id);
				if (seed == null || seed.IsDeleted) return false;

				// Xóa mềm bản ghi Seed
				var success = await _unitOfWork.Seeds.DeleteAsync(id);

				if (success)
				{
					// LOGIC QUAN TRỌNG: Khi xóa Seed chi tiết, 
					// bạn có muốn xóa luôn Product cha không? 
					// Nếu có thì thực hiện ở đây:
					var product = await _unitOfWork.Products.GetByIdAsync(seed.ProductId);
					if (product != null && !product.IsDeleted)
					{
						product.IsDeleted = true;
						product.DeletedAt = DateTime.UtcNow;
						_unitOfWork.Products.Update(product);
					}

					await _unitOfWork.CommitTransactionAsync();
					return true;
				}

				await _unitOfWork.RollbackTransactionAsync();
				return false;
			}
			catch (Exception)
			{
				await _unitOfWork.RollbackTransactionAsync();
				throw;
			}
		}
	}
}
