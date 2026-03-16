using AutoMapper.QueryableExtensions;
using DTO.DTOs.Products;
using Service.IServices.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Service.Services.Product
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		//Tạo mới sản phẩm.Có validate AnyAsync và Refetch dữ liệu sau khi tạo.
		public async Task<ProductDTO> CreateAsync(ProductCreateDTO dto)
		{
			// 1. Validate nhanh CategoryId có tồn tại không bằng AnyAsync
			var categoryExists = await _unitOfWork.Categories.AnyAsync(c => c.CategoryId == dto.CategoryId);
			if (!categoryExists) throw new KeyNotFoundException("Category ID does not exist.");

			// 2. Nếu có BrandId thì validate BrandId
			if (dto.BrandId.HasValue)
			{
				var brandExists = await _unitOfWork.Brands.AnyAsync(b => b.BrandId == dto.BrandId.Value);
				if (!brandExists) throw new KeyNotFoundException("Brand ID does not exist.");
			}

			// 3. Map và thêm mới
			var product = _mapper.Map<DAL.Entities.Product>(dto);
			product.IsActive = true;
			product.CreatedAt = DateTime.UtcNow;

			await _unitOfWork.Products.AddAsync(product);
			await _unitOfWork.SaveChangesAsync();

			// 4. TRICK: Gọi lại GetById để lấy DTO có đầy đủ CategoryName/BrandName trả về cho Frontend
			return await GetByIdAsync(product.ProductId)
				   ?? _mapper.Map<ProductDTO>(product);
		}
		/// Xóa mềm sản phẩm.
		public async Task<bool> DeleteAsync(int id)
		{
			var product = await _unitOfWork.Products.GetByIdAsync(id);

			if (product == null || product.IsDeleted)
				return false; 

			product.IsDeleted = true;
			product.DeletedAt = DateTime.UtcNow;

			_unitOfWork.Products.Update(product);
			await _unitOfWork.SaveChangesAsync() ;
			return true;
		}

		//get all product active
		public async Task<IEnumerable<ProductDTO>> GetAllActiveAsync()
		{
			//  dùng Queryable + ProjectTo để join lấy bảng luôn

			return await _unitOfWork.Products.GetQueryable()
				.Where(p => !p.IsDeleted && p.IsActive)
				.ProjectTo<ProductDTO>(_mapper.ConfigurationProvider) // Tự Join Category/Brand
				.ToListAsync();

		}

		//get all product 
		public async Task<IEnumerable<ProductDTO>> GetAllAsync()
		{
			//  dùng Queryable + ProjectTo để join lấy bảng luôn

			return await _unitOfWork.Products.GetQueryable()
				.Where(p => !p.IsDeleted)
				.ProjectTo<ProductDTO>(_mapper.ConfigurationProvider) // Tự Join Category/Brand
				.ToListAsync();
		}
		//lấy 1 sp chi tiết kèm category và brand
		public async Task<ProductDTO?> GetByIdAsync(int id)
		{
			return await _unitOfWork.Products.GetQueryable()
				.Where(p => p.ProductId == id && !p.IsDeleted)
				.ProjectTo<ProductDTO>(_mapper.ConfigurationProvider) // Tự động JOIN lấy Name
				.FirstOrDefaultAsync();
		}
		/// Bật/Tắt trạng thái kinh doanh của sản phẩm.
		public async Task<bool> ToggleActiveAsync(int productId, bool isActive)
		{
			var product = await _unitOfWork.Products.GetByIdAsync(productId);

			if (product == null || product.IsDeleted)
				throw new NotFoundException($"Product with ID {productId} not found.");

			product.IsActive = isActive;

			_unitOfWork.Products.Update(product);
			await _unitOfWork.SaveChangesAsync();

			return true;
		}

		public async Task<bool> UpdateAsync(int id, ProductUpdateDTO dto)
		{
			var product = await _unitOfWork.Products.GetByIdAsync(id);
			if (product == null || product.IsDeleted) return false;

			// Validate Category mới nếu cần
			var categoryExists = await _unitOfWork.Categories.AnyAsync(c => c.CategoryId == dto.CategoryId);
			if (!categoryExists) throw new KeyNotFoundException("Category ID does not exist.");

			// Map đè dữ liệu mới vào Entity cũ
			_mapper.Map(dto, product);
			product.UpdatedAt = DateTime.UtcNow;

			_unitOfWork.Products.Update(product);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}
	}
}
