using DAL.Entities;
using DTO.DTOs.Breeders;
using Service.IServices.Product;

namespace Service.Services.Product
{
	public class BreederService : IBreederService
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public BreederService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		// them 1 breeder moi
		public async Task<BreederDTO?> AddBreederAsync(BreederCreateDTO breederCreateDTO)
		{
			if (await BreederNameExistsAsync(breederCreateDTO.BreederName))
				throw new InvalidOperationException("Breeder name already exists.");
			var breeder = _mapper.Map<Breeder>(breederCreateDTO);
			breeder.CreatedAt = DateTime.UtcNow;
			await _unitOfWork.Breeders.AddAsync(breeder);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<BreederDTO>(breeder);
		}

		public async Task<bool> BreederNameExistsAsync(string breederName)
		{
			var breeder = await _unitOfWork.Breeders.FindAsync(
				x => x.BreederName.ToLower() == breederName.ToLower() && !x.IsDeleted
			);
			return breeder != null;

		}

		//tat ca breeder
		public async Task<IEnumerable<BreederDTO>> GetAllBreedersAsync()
		{
			var breeders = await _unitOfWork.Breeders.GetAllAsync();
			if (breeders == null || !breeders.Any())
			{
				return new List<BreederDTO>();
			}
			return _mapper.Map<IEnumerable<BreederDTO>>(breeders);
		}
		//get by id
		public async Task<BreederDTO?> GetBreederByIdAsync(int id)
		{
			var breeder = await _unitOfWork.Breeders.GetByIdAsync(id) ?? throw new NotFoundException($"Breeder with Id:{id} not found");
			return _mapper.Map<BreederDTO>(breeder);
		}
		//get by name
		public async Task<BreederDTO?> GetBreederByNameAsync(string breederName)
		{
			var breeder = await _unitOfWork.Breeders.FindAsync(n => n.BreederName == breederName) ?? throw new NotFoundException($"BreederName with name {breederName} not found");
			return _mapper.Map<BreederDTO>(breeder);

		}
		//update
		public async Task<bool> UpdateBreederAsync(int id, BreederUpdateDTO breederUpdateDTO)
		{
			var breeder = await _unitOfWork.Breeders.GetByIdAsync(id) ?? throw new NotFoundException($"Breeder with Id:{id} not found");
			_mapper.Map(breederUpdateDTO, breeder);
			breeder.UpdatedAt = DateTime.Now;
			if (await BreederNameExistsAsync(breederUpdateDTO.BreederName))
				throw new InvalidOperationException("Breeder name already exists.");
			_unitOfWork.Breeders.Update(breeder);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

	}
}
