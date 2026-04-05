
namespace DTO.Params
{
	public class QueryParam
	{
		public int Page { get; set; } = 1;
		public int Size { get; set; } = 10;
		public string? Search {get; set; } =null;
		public string? Sort =null;
	}
}
