using Role = DAL.Entities.Role;

namespace Service.Services
{
	public class RoleService : IRoleService
	{	private readonly IRoleRepository _repository;
		private readonly IMapper _mapper;
		public RoleService(IRoleRepository repository, IMapper mapper)
		{
			this._repository = repository;
			this._mapper = mapper;
		}
		// Add a new role
		public async Task<RoleDTO> AddRoleAsync(CreateRoleDTO createRoleDTO)
		{   //check if createRoleDTO is null
			ArgumentNullException.ThrowIfNull(createRoleDTO);

			var role = _mapper.Map<Role>(createRoleDTO);
			role.CreatedAt = DateTime.Now; // Set the creation timestamp
			var newRole = await _repository.AddAsync(role);
			return _mapper.Map<RoleDTO>(newRole);
		}
		// Get all roles
		public async Task<IEnumerable<RoleDTO>> GetAllRolesAsync()
		{
			var roles = await _repository.GetAllAsync();
			if (roles == null || !roles.Any())
			{
				return new List<RoleDTO>();
			}
			var rolesDTO = _mapper.Map<IEnumerable<RoleDTO>>(roles);
			return rolesDTO;
		}
		// Get role by ID
		public async Task<RoleDTO?> GetRoleByIdAsync(int id)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
			var role = await _repository.GetByIdAsync(id) ?? throw new NotFoundException($"Role with ID {id} not found.");
			var roleDTO = _mapper.Map<RoleDTO>(role);
			return roleDTO;
		}
		// Update role
		public async Task<RoleDTO?> UpdateRoleAsync(int id, RoleUpdateDTO updatedRoleDTO)
		{
			if (updatedRoleDTO == null)
			{
				throw new ArgumentNullException(nameof(updatedRoleDTO), "Updated role cannot be null.");
			}
			var existingRole = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Role with ID {id} not found.");
			var updatedRole = _mapper.Map<Role>(updatedRoleDTO);
			var updated = await _repository.UpdateAsync(id, updatedRole);
			if (updated == null)
			{
				throw new Exception("Failed to update role.");
			}
			return _mapper.Map<RoleDTO>(updated) ?? throw new Exception("Failed to map updated role."); 
		}
	}
}
