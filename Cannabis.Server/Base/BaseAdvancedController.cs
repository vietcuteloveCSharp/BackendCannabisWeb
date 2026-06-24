
namespace Cannabis.Server.Base
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	public abstract class BaseAdvancedController<TEntity, TReadDto, TCreateDto, TUpdateDto> : BaseCrudController<TEntity, TReadDto, TCreateDto, TUpdateDto>
		where TEntity : class
		where TReadDto : class
		where TCreateDto : class
		where TUpdateDto : class
	{

		protected BaseAdvancedController(IBaseCRUDService<TEntity, TReadDto, TCreateDto, TUpdateDto> service) :base(service) 
		{
			
		}
		

		[HttpDelete("{id:int}/hard")]
		[Authorize(Roles = "Admin")]
		public virtual async Task<IActionResult> HardDelete(int id)
		{
			var result = await _service.HardDeleteAsync(id);
			return result.Success ? Ok(result) : BadRequest(result);
		}
		// 2. XÓA VĨNH VIỄN HÀNG LOẠT
		[HttpDelete("hard-bulk")]
		[Authorize(Roles = "Admin")]
		public virtual async Task<IActionResult> HardDeleteMany([FromBody] List<int> ids)
		{
			if (ids == null || !ids.Any()) return BadRequest("Danh sách ID không hợp lệ.");

			var result = await _service.HardDeleteManyAsync(ids);
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
			if (ids == null || !ids.Any()) return BadRequest("Danh sách ID không hợp lệ.");
			var result = await _service.RestoreManyAsync(ids);
			return result.Success ? Ok(result) : BadRequest(result);
		}
	}
}
