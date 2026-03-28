namespace Service.IServices
{
	public interface IRoleService
	{
		Task<IEnumerable<RoleDTO>> GetAllRolesAsync();
		Task<RoleDTO?> GetRoleByIdAsync(int id);
		Task<RoleDTO> AddRoleAsync(RoleCreateDTO createRoleDTO);
		Task<RoleDTO?> UpdateRoleAsync(int id, RoleUpdateDTO updatedRole);
	}
}
