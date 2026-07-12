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
			Items = items??Enumerable.Empty<T>(); //chống nulll
			TotalCount = count;
			PageNumber = pageNumber; //đảm bảo page tối thiểu là 1 
			PageSize = pageSize; /// Nếu pageSize <= 0, tự động đưa về default là 10 để tránh chia cho 0
		}
	}
}
