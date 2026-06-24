namespace Cannabis.Server.Base
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	public abstract class BaseReadOnlyController<TEntity,TReadDTO> : ControllerBase
		where TEntity : class
		where TReadDTO : class
	{
		protected readonly IBaseReadOnlyService<TEntity, TReadDTO> _readOnlyService;
		protected BaseReadOnlyController(IBaseReadOnlyService<TEntity, TReadDTO> readOnlyService)
		{
			_readOnlyService = readOnlyService;
		}
		[HttpGet]
		public virtual async Task<IActionResult> GetAll([FromQuery] QueryParam query)
		{
			var result = await _readOnlyService.GetPagedAsync(query);
			return Ok(result);
		}
		[HttpGet("{id:int}")]
		public virtual async Task<IActionResult> GetById(int id)
		{
			var result = await _readOnlyService.GetByIdAsync(id);
			return result.Success ? Ok(result) : NotFound(result);
		}
	}
}
