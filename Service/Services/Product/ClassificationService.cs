using Service.IServices.Product;
using Service.Services.BaseService;

namespace Service.Services
{
	public class ClassificationService : BaseService<Classification,ClassificationDTO,ClassificationCreateDTO,ClassificationUpdateDTO> ,IClassificationService
	{	
		public ClassificationService(IUnitOfWork unitOfWork, IMapper mapper) :base(unitOfWork,mapper)
		{
			
		}
		

	}
}
