namespace Service.Interfaces.Internal
{
	public interface IRoleService : IBaseCRUDService<DAL.Entities.Internal.Role, RoleDTO, RoleCreateDTO, RoleUpdateDTO>
	{
		Task<ApiResponse<RoleDTO>> GetByNameAsync(string roleName);
	}
}
