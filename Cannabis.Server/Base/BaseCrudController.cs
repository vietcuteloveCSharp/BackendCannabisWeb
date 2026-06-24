
namespace Cannabis.Server.Base
{
	public abstract class BaseCrudController<TEntity, TReadDto, TCreateDto, TUpdateDto> :BaseReadOnlyController<TEntity,TReadDto>
		where TEntity : class
		where TReadDto : class
		where TCreateDto : class
		where TUpdateDto : class
	{
		protected readonly IBaseCRUDService<TEntity, TReadDto, TCreateDto, TUpdateDto> _service;
		protected BaseCrudController(IBaseCRUDService<TEntity, TReadDto, TCreateDto, TUpdateDto> service) : base(service) 
		{
			_service = service;
		}
		[HttpPost]
		public virtual async Task<IActionResult> Create([FromBody] TCreateDto dto)
		{
			if (dto == null) return BadRequest("Dữ liệu đầu vào không hợp lệ.");

			var result = await _service.CreateAsync(dto);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpPut("{id:int}")]
		public virtual async Task<IActionResult> Update(int id, [FromBody] TUpdateDto dto)
		{

			var result = await _service.UpdateAsync(id, dto);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("{id:int}")]
		public virtual async Task<IActionResult> Delete(int id)
		{
			var result = await _service.SoftDeleteAsync(id);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("bulk")]
		public virtual async Task<IActionResult> DeleteMany([FromBody] List<int> ids)
		{
			if (ids == null || !ids.Any()) return BadRequest("Danh sách ID không hợp lệ.");

			var result = await _service.DeleteManyAsync(ids);
			return result.Success ? Ok(result) : BadRequest(result);
		}
	}
}
