using DAL.Entities;
using DTO.DTOs.Breeders;
using DTO.Response;
using Service.IServices.Product;
using Service.Services.BaseService;

namespace Service.Services.Product
{
	public class BreederService : BaseService<Breeder,BreederDTO,BreederCreateDTO,BreederUpdateDTO>, IBreederService
	{

		public BreederService(IUnitOfWork unitOfWork, IMapper mapper) :base(unitOfWork,mapper)
		{
				
		}

		//update
		public override async Task<ApiResult> UpdateAsync(int id, BreederUpdateDTO breederUpdateDTO)
		{
			var breeder = await _repository.GetByIdAsync(id) ?? throw new NotFoundException($"Breeder with Id:{id} not found");
			_mapper.Map(breederUpdateDTO, breeder);

			breeder.UpdatedAt = DateTime.Now;

			var isDuplicate = await _repository.AnyAsync(x =>
			x.BreederName.ToLower() == breederUpdateDTO.BreederName.ToLower() && x.Id != id);

			if (isDuplicate)
			{
				return ApiResult.Fail("Tên nhà nhân giống này đã tồn tại.");
			}
				
			_unitOfWork.Breeders.Update(breeder);
			var result = await _unitOfWork.SaveChangesAsync();

			// 5. Trả về kết quả - KHÔNG gọi base nữa
			return result > 0
				? ApiResult.Ok("Cập nhật thành công.")
				: ApiResult.Fail("Không có thay đổi nào được thực hiện.");
		}
	}
}
