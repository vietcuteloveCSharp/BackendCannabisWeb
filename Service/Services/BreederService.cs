using DTO.DTOs.Breeders;

namespace Service.Services
{
	public class BreederService :IBreederService
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public BreederService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		// create breeder
		public async Task<BreederDTO> CreateBreederAsync(BreederCreateDTO dto)
		{
			ArgumentNullException.ThrowIfNull(dto, nameof(dto));
			var breeder = _mapper.Map<Breeder>(dto);
			breeder.CreatedAt = DateTime.Now;
			await _unitOfWork.Breeders.AddAsync(breeder);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<BreederDTO>(breeder);


		}
		//get all breeders
		public async Task<IEnumerable<BreederDTO>> GetAllBreederAsync()
		{
			var breeders = await _unitOfWork.Breeders.GetAllAsync();
			if (breeders == null || !breeders.Any())
			{
				return new List<BreederDTO>();
			}
			return _mapper.Map<IEnumerable<BreederDTO>>(breeders);
		}
		//get breeder by id
		public async Task<BreederDTO> GetBreederByIdAsync(int id)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
			var breeder = await _unitOfWork.Breeders.GetByIdAsync(id) ?? throw new NotFoundException($"Breeder with Id:{id} not found");
			return _mapper.Map<BreederDTO>(breeder);

		}
		//update breeder
		public async Task<BreederDTO> UpdateBreederAsync(int id, BreederUpdateDTO dto)
		{
			var breeder = await _unitOfWork.Breeders.GetByIdAsync(id) ?? throw new NotFoundException($"Breeder with Id:{id} not found");
			_mapper.Map(dto, breeder);
			breeder.UpdatedAt = DateTime.Now;
			await _unitOfWork.Breeders.UpdateAsync(id,breeder);
			await _unitOfWork.SaveChangesAsync();
			 return _mapper.Map<BreederDTO>(breeder);

		}
	}
}
