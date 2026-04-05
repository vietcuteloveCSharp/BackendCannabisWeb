namespace DTO.Response
{
	public class ApiResponse<T> :ApiResult
	{
		public T? Data { get; set; }


		// 1. Dùng cho lấy 1 đối tượng hoặc trả về sau khi tạo mới (Không phân trang)
		public static ApiResponse<T> Ok(T data, string message = "")
		{
			return new ApiResponse<T>
			{
				Success = true,
				Data = data,
				Message = message
			};
		}

		// 2. Dùng riêng cho lấy Danh sách (Có phân trang)
		public static ApiResponse<T> Paged(T data, string message = "")
		{
			return new ApiResponse<T>
			{
				Success = true,
				Data = data,
				Message = message
			};
		}

		// 3. Ghi đè hàm Fail để trả về đúng kiểu ApiResponse<T>
		public new static ApiResponse<T> Fail(string message, params string[] errors)
			=> new() { Success = false, Message = message, Errors = errors };
	}
}
