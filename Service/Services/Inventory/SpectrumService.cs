using DTO.Response;
using Service.IServices.Shared;
using Service.Services.BaseService;
using System.Linq.Expressions;

namespace Service.Services.Inventory
{
	public class SpectrumService : BaseService<Spectrum, SpectrumDTO, SpectrumCreateDTO, SpectrumUpdateDTO>,ISpectrumService
	{
		
		private readonly IFileService _fileService;
		public SpectrumService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService) : base(unitOfWork, mapper)
		{
			
			_fileService = fileService;
		}
		public override async Task<ApiResult> CreateAsync(SpectrumCreateDTO createSpectrumDTO)
		{
			var entity = _mapper.Map<Spectrum>(createSpectrumDTO);
			string uploadedPath = null;
			// 1. Mở Transaction để đảm bảo tính toàn vẹn
			using var transaction = await _unitOfWork.BeginTransactionAsync();
			try
			{
				if (createSpectrumDTO.ChartFile != null)
				{
					uploadedPath = await _fileService.UploadFileAsync(createSpectrumDTO.ChartFile, nameof(Spectrum));
					entity.SpectrumChartUrl = uploadedPath;
				}

				await _repository.AddAsync(entity);
				await _unitOfWork.SaveChangesAsync();

				await transaction.CommitAsync(); // Mọi thứ OK
				return ApiResult.Ok("Thành công");
				// Nếu SaveChanges trả về 0, ta chủ động quăng lỗi để chạy vào catch dọn dẹp
				throw new Exception("Database không có thay đổi nào.");
			}
			catch (Exception)
			{
				// 5. CÓ LỖI: Rollback Database ngay lập tức
				await transaction.RollbackAsync();

				// 6. Xóa file rác đã upload (vì DB không lưu được, để lại file sẽ gây rác server)
				if (!string.IsNullOrEmpty(uploadedPath))
				{
					_fileService.DeleteFile(uploadedPath);
				}

				// 7. Ném lỗi ra cho Middleware của bạn xử lý (Log, trả về lỗi 500)
				throw;

			}


		}

		public override async Task<ApiResult> UpdateAsync(int id, SpectrumUpdateDTO updateSpectrumDTO)
		{
			var entity = await _repository.GetByIdAsync(id);
			if (entity == null)
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
			var updated = _unitOfWork.Spectrums.Update(entity);
			await _unitOfWork.SaveChangesAsync();
			return ApiResult.Ok("Update thành công");
		}
		
	}
}
