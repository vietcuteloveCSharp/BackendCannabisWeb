


namespace Service.Implementations.BaseService
{
	public  abstract class BaseCRUDService<TEntity, TReadDto, TCreateDto, TUpdateDto> : BaseReadOnlyService<TEntity, TReadDto>,IBaseCRUDService<TEntity, TReadDto, TCreateDto, TUpdateDto>
		where TEntity : class
		where TReadDto : class
		where TCreateDto : class
		where TUpdateDto : class
	{
		protected BaseCRUDService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork,mapper)
		{
			
		}
		public virtual async Task<ApiResult> CreateAsync(TCreateDto dto)
		{
			var entity = _mapper.Map<TEntity>(dto);
			await _repository.AddAsync(entity);
			var result = await _unitOfWork.SaveChangesAsync();

			return result > 0 ? ApiResult.Ok("Thêm mới thành công.") : ApiResult.Fail("Thêm mới thất bại.");
		}

		public virtual async Task<ApiResult> UpdateAsync(int id, TUpdateDto dto)
		{
			var existingEntity = await _repository.GetByIdAsync(id);
			if (existingEntity == null) return ApiResult.Fail("Không tìm thấy dữ liệu.");

			_mapper.Map(dto, existingEntity);
			_repository.Update(existingEntity);
			var result = await _unitOfWork.SaveChangesAsync();

			return result > 0 ? ApiResult.Ok("Cập nhật thành công.") : ApiResult.Fail("Không có thay đổi nào.");
		}

		public virtual async Task<ApiResult> SoftDeleteAsync(int id)
		{
			var success = await _repository.DeleteAsync(id); // Giả định Repo đã xử lý IsDeleted = true
			if (!success) return ApiResult.Fail("Không tìm thấy hoặc đã bị xóa.");

			return await _unitOfWork.SaveChangesAsync() > 0
				? ApiResult.Ok("Xóa tạm thành công.")
				: ApiResult.Fail("Lỗi khi xóa dữ liệu.");
		}

		public virtual async Task<ApiResult> SoftDeleteManyAsync(List<int> ids)
		{
			if (ids == null || !ids.Any()) return ApiResult.Fail("ID không hợp lệ.");

			var affected = await _repository.ExecuteUpdateBatchAsync(
				 x => ids.Contains(EF.Property<int>(x, "Id")) && EF.Property<bool>(x, "IsDeleted") == false,
				 s => s
					.SetProperty(e => EF.Property<bool>(e, "IsDeleted"), true)
					.SetProperty(e => EF.Property<DateTime?>(e, "DeletedAt"), DateTime.UtcNow)
			);

			return affected > 0 ? ApiResult.Ok($"Đã xóa tạm {affected} bản ghi.") : ApiResult.Fail("Không có bản ghi nào bị ảnh hưởng.");
		}

		public virtual async Task<ApiResult> HardDeleteAsync(int id)
		{
			var affected = await _repository.ExecuteDeleteBatchAsync(x => EF.Property<int>(x, "Id") == id);
			return affected > 0 ? ApiResult.Ok("Xóa vĩnh viễn thành công.") : ApiResult.Fail("Không tìm thấy dữ liệu.");
		}

		public virtual async Task<ApiResult> HardDeleteManyAsync(List<int> ids)
		{
			var affected = await _repository.ExecuteDeleteBatchAsync(x => ids.Contains(EF.Property<int>(x, "Id")));
			return affected > 0 ? ApiResult.Ok($"Xóa vĩnh viễn {affected} bản ghi.") : ApiResult.Fail("Thất bại.");
		}

		public virtual async Task<ApiResult> RestoreAsync(int id)
		{
			var affected = await _repository.ExecuteUpdateBatchAsync(
				x => EF.Property<int>(x, "Id") == id && EF.Property<bool>(x, "IsDeleted") == true,
				s => s
					.SetProperty(e => EF.Property<bool>(e, "IsDeleted"), false)
					.SetProperty(e => EF.Property<DateTime?>(e, "DeletedAt"), (DateTime?)null)
			);
			return affected > 0 ? ApiResult.Ok("Khôi phục thành công.") : ApiResult.Fail("Dữ liệu không cần khôi phục.");
		}

		public virtual async Task<ApiResult> RestoreManyAsync(List<int> ids)
		{
			var affected = await _repository.ExecuteUpdateBatchAsync(
				x => ids.Contains(EF.Property<int>(x, "Id")) && EF.Property<bool>(x, "IsDeleted") == true,
				s => s
					.SetProperty(e => EF.Property<bool>(e, "IsDeleted"), false)
					.SetProperty(e => EF.Property<DateTime?>(e, "DeletedAt"), (DateTime?)null)
			);
			return affected > 0 ? ApiResult.Ok($"Khôi phục {affected} bản ghi.") : ApiResult.Fail("Thất bại.");
		}

		public virtual Task<ApiResult> DeleteManyAsync(List<int> ids) => SoftDeleteManyAsync(ids);

		public async Task<ApiResponse<bool>> ExistsAsync(int id)
		{
			// Gọi hàm AnyAsync đã có sẵn trong BaseRepository của bạn
			var exists = await _repository.AnyAsync(x => EF.Property<int>(x, "Id") == id);

			return ApiResponse<bool>.Ok(exists, exists ? "Đối tượng tồn tại" : "Đối tượng không tồn tại");
		}
	}
}
