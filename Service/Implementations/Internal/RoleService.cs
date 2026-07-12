namespace Service.Implementations.Internal
{
	public class RoleService : BaseCRUDService<DAL.Entities.Internal.Role, RoleDTO, RoleCreateDTO, RoleUpdateDTO>, IRoleService
	{
		public RoleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
		{
		}

		public async Task<ApiResponse<RoleDTO>> GetByNameAsync(string roleName)
		{
			var role = await _unitOfWork.Roles.GetByNameAsync(roleName);
			if (role == null)
			{
				return ApiResponse<RoleDTO>.Fail("Không tìm thấy quyền hạn.");
			}

			var dto = _mapper.Map<RoleDTO>(role);
			return ApiResponse<RoleDTO>.Ok(dto);
		}

		// Role không kế thừa ISoftDelete nên override SoftDeleteManyAsync để dùng HardDeleteManyAsync tránh lỗi lambda EF Core
		public override async Task<ApiResult> SoftDeleteManyAsync(List<int> ids)
		{
			return await HardDeleteManyAsync(ids);
		}

		public override Task<ApiResult> RestoreAsync(int id)
		{
			return Task.FromResult(ApiResult.Fail("Quyền hạn không hỗ trợ khôi phục."));
		}

		public override Task<ApiResult> RestoreManyAsync(List<int> ids)
		{
			return Task.FromResult(ApiResult.Fail("Quyền hạn không hỗ trợ khôi phục."));
		}

		// Hỗ trợ tìm kiếm theo tên hoặc mô tả
		protected override Expression<Func<DAL.Entities.Internal.Role, bool>>? BuildSearchFilter(string search)
		{
			return r => r.RoleName.Contains(search) || (r.Description != null && r.Description.Contains(search));
		}
	}
}
