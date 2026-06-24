namespace Shared.Common.Response
{
	public class PagedResult <T>
	{	 //gán các giá trị phân trang
		public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
		public int TotalCount { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
		public PagedResult(IEnumerable<T> items, int count, int pageNumber, int pageSize)
		{
			Items = items;
			TotalCount = count;
			PageNumber = pageNumber;
			PageSize = pageSize;
		}
	}
}
