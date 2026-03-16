using DAL.Entities;
using DTO.DTOs.Breeders;
using Service.IServices.Product;

namespace Service.Services.Product
{
	public class BreederService(IUnitOfWork unitOfWork, IMapper mapper) : IBreederService
	{

		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly IMapper _mapper = mapper;

		// them 1 breeder moi
		public async Task<BreederDTO?> AddAsync(BreederCreateDTO breederCreateDTO)
		{
			if (await NameExistsAsync(breederCreateDTO.BreederName))
				throw new InvalidOperationException("Breeder name already exists.");
			var breeder = _mapper.Map<Breeder>(breederCreateDTO);
			breeder.CreatedAt = DateTime.UtcNow;
			await _unitOfWork.Breeders.AddAsync(breeder);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<BreederDTO>(breeder);
		}

		public async Task<bool> NameExistsAsync(string breederName)
		{
			var breeder = await _unitOfWork.Breeders.FindAsync(
				x => x.BreederName.ToLower() == breederName.ToLower() && !x.IsDeleted
			);
			return breeder != null;

		}

		public async Task<bool> DeleteAsync(int id)
		{
			var entity = await _unitOfWork.Breeders.GetByIdAsync(id);

			if (entity == null || entity.IsDeleted)
				return false;

			entity.IsDeleted = true;
			entity.DeletedAt = DateTime.UtcNow;

			_unitOfWork.Breeders.Update(entity);
			await _unitOfWork.SaveChangesAsync();

			return true;
		}

		public Task<IEnumerable<BreederDTO>> GetAllActiveAsync()
		{
			throw new NotImplementedException();
		}

		//tat ca breeder
		public async Task<IEnumerable<BreederDTO>> GetAllAsync()
		{
			var breeders = await _unitOfWork.Breeders.GetAllAsync();
			if (breeders == null || !breeders.Any())
			{
				return new List<BreederDTO>();
			}
			return _mapper.Map<IEnumerable<BreederDTO>>(breeders);
		}
		//get by id
		public async Task<BreederDTO?> GetByIdAsync(int id)
		{
			var breeder = await _unitOfWork.Breeders.GetByIdAsync(id) ?? throw new NotFoundException($"Breeder with Id:{id} not found");
			return _mapper.Map<BreederDTO>(breeder);
		}
		//get by name
		public async Task<BreederDTO?> GetByNameAsync(string breederName)
		{
			var breeder = await _unitOfWork.Breeders.FindAsync(n => n.BreederName == breederName) ?? throw new NotFoundException($"BreederName with name {breederName} not found");
			return _mapper.Map<BreederDTO>(breeder);

		}
		//update
		public async Task<bool> UpdateAsync(int id, BreederUpdateDTO breederUpdateDTO)
		{
			var breeder = await _unitOfWork.Breeders.GetByIdAsync(id) ?? throw new NotFoundException($"Breeder with Id:{id} not found");
			_mapper.Map(breederUpdateDTO, breeder);
			breeder.UpdatedAt = DateTime.Now;
			if (await NameExistsAsync(breederUpdateDTO.BreederName))
				throw new InvalidOperationException("Breeder name already exists.");
			_unitOfWork.Breeders.Update(breeder);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

		public Task<bool> ExistAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
