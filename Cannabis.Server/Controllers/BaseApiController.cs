using Microsoft.AspNetCore.Mvc;

namespace Cannabis.Server.Controllers
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	public abstract class BaseApiController<TEntity, TReadDto, TCreateDto, TUpdateDto> : ControllerBase
		where TEntity : class
		where TReadDto : class
		where TCreateDto : class
		where TUpdateDto : class
	{
		protected readonly IBaseService<TEntity, TReadDto, TCreateDto, TUpdateDto> _service;
		protected BaseApiController(IBaseService<TEntity, TReadDto, TCreateDto, TUpdateDto> service)
		{
			_service = service;
		}
		[HttpGet]
		public virtual async Task<IActionResult> GetAll([FromQuery] QueryParam query)
		{
			var result = await _service.GetPagedAsync(query);
			return Ok(result);
		}
		[HttpGet("{id:int}")]
		public virtual async Task<IActionResult> GetById(int id)
		{
			var result = await _service.GetByIdAsync(id);
			return result.Success ? Ok(result) : NotFound(result);
		}

		[HttpPost]
		public virtual async Task<IActionResult> Create([FromBody] TCreateDto dto)
		{
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
			var result = await _service.DeleteManyAsync(ids);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpDelete("{id:int}/hard")]
		[Authorize(Roles = "Admin")]
		public virtual async Task<IActionResult> HardDelete(int id)
		{
			var result = await _service.HardDeleteAsync(id);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpPost("{id:int}/restore")]
		public virtual async Task<IActionResult> Restore(int id)
		{
			var result = await _service.RestoreAsync(id);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpPost("restore")]
		public virtual async Task<IActionResult> RestoreMany([FromBody] List<int> ids)
		{
			var result = await _service.RestoreManyAsync(ids);
			return result.Success ? Ok(result) : BadRequest(result);
		}
	}
}
