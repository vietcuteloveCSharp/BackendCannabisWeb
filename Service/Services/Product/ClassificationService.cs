
using DTO.DTOs.Breeders;
using Service.IServices.Product;

namespace Service.Services
{
	public class ClassificationService : IClassificationService
	{	private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public ClassificationService(IUnitOfWork unitOfWork, IMapper _mapper)
		{
			this._unitOfWork = unitOfWork;
			this._mapper = _mapper;
		}
		// Create a new classification
		public async Task<ClassificationDTO> CreateAsync(ClassificationCreateDTO dto)
		{
			if (await NameExistsAsync(dto.ClassificationName))
				throw new InvalidOperationException("Classification name already exists.");
			var entity = _mapper.Map<Classification>(dto);
			await _unitOfWork.Classifications.AddAsync(entity);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<ClassificationDTO>(entity);
		}
		// Get all classifications
		public async Task<IEnumerable<ClassificationDTO>> GetAllAsync()
		{
			var classifications = await _unitOfWork.Classifications.GetAllAsync();
			if (classifications == null)
			{
				return new List<ClassificationDTO>();
			}
			return _mapper.Map<List<ClassificationDTO>>(classifications);
		}
		// Get classification by ID
		public async Task<ClassificationDTO?> GetByIdAsync(int id)
		{	
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
			var entity = await _unitOfWork.Classifications.GetByIdAsync(id);
			if(entity == null)
			{
				throw new NotFoundException($"Classification with ID {id} not found.");
			}
			return _mapper.Map<ClassificationDTO>(entity);
		}
		// Update classification
		public async Task<bool> UpdateAsync(int id, ClassificationUpdateDTO dto)
		{
			var entity = await _unitOfWork.Classifications.GetByIdAsync(id);
			if (entity == null)
			{
				throw new NotFoundException($"Classification with ID {id} not found.");
			}
			_mapper.Map(dto, entity);
			entity.UpdatedAt = DateTime.Now; // Update the timestamp
			 _unitOfWork.Classifications.Update(entity);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}
		public async Task<bool> NameExistsAsync(string classifiName)
		{
			var Classification = await _unitOfWork.Classifications.FindAsync(
				x => x.ClassificationName.ToLower() == classifiName.ToLower() && !x.IsDeleted
			);
			return Classification != null;

		}

		public async Task<IEnumerable<ClassificationDTO>> GetAllActiveAsync()
		{
			var classifications = await _unitOfWork.Classifications.GetAllActiveAsync();
			if (classifications == null)
			{
				return new List<ClassificationDTO>();
			}
			return _mapper.Map<List<ClassificationDTO>>(classifications);
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var entity = await _unitOfWork.Classifications.GetByIdAsync(id);

			if (entity == null || entity.IsDeleted)
				return false;

			entity.IsDeleted = true;
			entity.DeletedAt = DateTime.UtcNow;

			_unitOfWork.Classifications.Update(entity);
			await _unitOfWork.SaveChangesAsync();

			return true;
		}
	}
}
