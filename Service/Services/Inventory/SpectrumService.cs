using Service.IServices.Shared;

namespace Service.Services.Inventory
{
	public class SpectrumService : ISpectrumService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IFileService _fileService;
		public SpectrumService(IUnitOfWork unitOfWork, IMapper mapper,IFileService fileService)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_fileService = fileService;
		}
		// add a new spectrum
		public async Task<SpectrumDTO> AddAsync(SpectrumCreateDTO createSpectrumDTO)
		{
			var entity = _mapper.Map<Spectrum>(createSpectrumDTO);
			// 2. Xử lý lưu File nếu có gửi kèm
			if (createSpectrumDTO.ChartFile != null)
			{

				// Gọi hàm Upload dùng chung, lưu vào folder "Spectrum"
				// Kết quả trả về là string: "/uploads/Spectrum/guid_name.png"
				string folderName = "Spectrum";
				string filePath = await _fileService.UploadFileAsync(createSpectrumDTO.ChartFile, folderName);

				// Gán đường dẫn vào Entity trước khi lưu DB
				entity.SpectrumChartUrl = filePath;
			}
			var added = await _unitOfWork.Spectrums.AddAsync(entity);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<SpectrumDTO>(added);
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var entity = await _unitOfWork.Spectrums.GetByIdAsync(id);

			if (entity == null || entity.IsDeleted)
				return false;

			entity.IsDeleted = true;
			entity.DeletedAt = DateTime.UtcNow;

			_unitOfWork.Spectrums.Update(entity);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

		public async Task<bool> ExistAsync(int id)
		{
			var entity = await _unitOfWork.Spectrums.GetByIdAsync(id);
			return entity != null && !entity.IsDeleted;
		}

		public async Task<IEnumerable<SpectrumDTO?>> GetAllActiveAsync()
		{
			var spectrums = await _unitOfWork.Spectrums.GetAllActiveAsync();
			if (spectrums == null || !spectrums.Any())
			{
				return new List<SpectrumDTO>();
			}
			return _mapper.Map<IEnumerable<SpectrumDTO>>(spectrums);
		}

		// get all spectrums
		public async Task<IEnumerable<SpectrumDTO?>> GetAllAsync()
		{
			var spectrums = await _unitOfWork.Spectrums.GetAllAsync();
			if (spectrums == null || !spectrums.Any())
			{
				return new List<SpectrumDTO>();
			}
			return _mapper.Map<IEnumerable<SpectrumDTO>>(spectrums);
		}
		// get spectrum by id
		public async Task<SpectrumDTO?> GetByIdAsync(int id)
		{
			
			var spectrum = await _unitOfWork.Spectrums.GetByIdAsync(id);
			if (spectrum == null)
			{
				throw new NotFoundException($"Spectrum with ID {id} not found.");
			}
			return _mapper.Map<SpectrumDTO>(spectrum);
		}
		// update spectrum
		public async Task<bool> UpdateAsync(int id, SpectrumUpdateDTO updateSpectrumDTO)
		{
			var entity = await _unitOfWork.Spectrums.GetByIdAsync(id);
			if (entity  == null)
			{
				throw new NotFoundException($"Spectrum with ID {id} not found.");
			}
			_mapper.Map(updateSpectrumDTO, entity);
			if (updateSpectrumDTO.ChartFile != null)
			{
				_fileService.DeleteFile(entity.SpectrumChartUrl!);
				// Gọi hàm Upload dùng chung, lưu vào folder "Spectrum"
				// Kết quả trả về là string: "/uploads/Spectrum/guid_name.png"
				string folderName = "Spectrum";
				string filePath = await _fileService.UploadFileAsync(updateSpectrumDTO.ChartFile, folderName);

				// Gán đường dẫn vào Entity trước khi lưu DB
				entity.SpectrumChartUrl = filePath;
			}
			var updated =  _unitOfWork.Spectrums.Update(entity);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}
	}
}
