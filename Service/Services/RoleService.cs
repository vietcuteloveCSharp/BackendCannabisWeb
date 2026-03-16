

namespace Service.Services
{
	public class RoleService : IRoleService
	{	private IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this._unitOfWork = unitOfWork;
			this._mapper = mapper;
		}
		// Add a new role
		public async Task<RoleDTO> AddRoleAsync(RoleCreateDTO createRoleDTO)
		{   //check if createRoleDTO is null
			ArgumentNullException.ThrowIfNull(createRoleDTO);
			var role = _mapper.Map<DAL.Entities.Role>(createRoleDTO);
			role.CreatedAt = DateTime.Now; // Set the creation timestamp
			var newRole = await _unitOfWork.Roles.AddAsync(role);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<RoleDTO>(newRole);
		}
		// Get all roles
		public async Task<IEnumerable<RoleDTO>> GetAllRolesAsync()
		{
			var roles = await _unitOfWork.Roles.GetAllAsync();
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
			var role = await _unitOfWork.Roles.GetByIdAsync(id) ?? throw new NotFoundException($"Role with ID {id} not found.");
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
			var existingRole = await _unitOfWork.Roles.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Role with ID {id} not found.");
			var updatedRole = _mapper.Map(updatedRoleDTO,existingRole);
			updatedRole.UpdatedAt = DateTime.UtcNow;
			await _unitOfWork.SaveChangesAsync();
			
			return _mapper.Map<RoleDTO>(existingRole) ?? throw new Exception("Failed to map updated role."); 
		}
		//get role active
		public async Task<IEnumerable<RoleDTO>> GetAllRolesActiveAsync()
		{
			var roles = await _unitOfWork.Roles.GetAllActiveAsync();
			if (roles == null || !roles.Any())
			{
				return new List<RoleDTO>();
			}
			var rolesDTO = _mapper.Map<IEnumerable<RoleDTO>>(roles);
			return rolesDTO;
		}
	}
}
