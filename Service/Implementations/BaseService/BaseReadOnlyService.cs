namespace Service.Services.BaseService
{
	public abstract class BaseReadOnlyService<TEntity, TReadDto> : IBaseReadOnlyService<TEntity, TReadDto>
		where TEntity : class
		where TReadDto : class
		
	{
		protected readonly IUnitOfWork _unitOfWork;
		protected readonly IMapper _mapper;
		protected readonly IBaseRepository<TEntity> _repository;
		protected BaseReadOnlyService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			// Tự động tìm đúng Repository tương ứng với Entity
			_repository = _unitOfWork.Repository<TEntity>();
		}
		//get all
		public virtual async Task<ApiResponse<PagedResult<TReadDto>>> GetPagedAsync(QueryParam query)
		{
			Expression<Func<TEntity, bool>>? filter = null;
			// Nếu có search, ta build predicate ở đây
			if (!string.IsNullOrWhiteSpace(query.Search))
			{
				filter = BuildSearchFilter(query.Search);
			}
			// 1. Gọi Repo lấy dữ liệu phân trang thô
			var pagedResult = await _repository.GetPagedAsync(query.Page, query.Size, filter);

			// 2. Map danh sách Entity sang DTO
			var dtos = _mapper.Map<IEnumerable<TReadDto>>(pagedResult.Items);
			//đóng gói vào paged reuslt
			var pagedDto = new PagedResult<TReadDto>(dtos, pagedResult.TotalCount, query.Page, query.Size);

			// 3. Đóng gói vào ApiResponse (Dùng hàm Paged đã viết ở DTO)
			return ApiResponse<PagedResult<TReadDto>>.Ok(pagedDto, "Lấy dữ liệu thành công");
		}
		
		// 1. LẤY CHI TIẾT THEO ID
		public virtual async Task<ApiResponse<TReadDto>> GetByIdAsync(int id)
		{
			var entity = await _repository.GetByIdAsync(id);

			if (entity == null)
				return ApiResponse<TReadDto>.Fail("Không tìm thấy dữ liệu yêu cầu.");

			var dto = _mapper.Map<TReadDto>(entity);
			return ApiResponse<TReadDto>.Ok(dto);
		}

		// get all k có phân trang
		public virtual async Task<ApiResponse<IEnumerable<TReadDto>>> GetAllAsync()
		{
			var entities = await _repository.GetAllAsync();
			var dtos = _mapper.Map<IEnumerable<TReadDto>>(entities);
			return ApiResponse<IEnumerable<TReadDto>>.Ok(dtos, "Lấy tất cả dữ liệu thành công");
		}
		protected virtual Expression<Func<TEntity, bool>>? BuildSearchFilter(string search) => null;

	}
}

