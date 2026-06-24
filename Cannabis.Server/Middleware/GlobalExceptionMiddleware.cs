

namespace Middleware
{
	public class GlobalExceptionMiddleware
	{
		private readonly ILogger<GlobalExceptionMiddleware> _logger;
		private readonly RequestDelegate _next;
		public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> _logger)
		{
			this._next = next;
			this._logger = _logger;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context); //tiếp tục pipieline
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex); // xử lý exception
			}
		}

		private  Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			//phân loại lỗi
			HttpStatusCode code = HttpStatusCode.InternalServerError;
			ApiResponse<object> apiResponse;
			switch (ex)
			{
				case ValidationException vex:
					code = HttpStatusCode.BadRequest;
					apiResponse = ApiResponse<object>.Fail(
					"Validation failed",
					vex.Message.Split("; ") // tách các lỗi từng field
					);
				break;
				case NotFoundException:
					code = HttpStatusCode.NotFound;
					apiResponse = ApiResponse<object>.Fail(ex.Message);
					break;
				case UnauthorizedAccessException:
					code = HttpStatusCode.Unauthorized;
					apiResponse = ApiResponse<object>.Fail(ex.Message);
					break;
				case ArgumentException:
					code = HttpStatusCode.BadRequest;
					apiResponse = ApiResponse<object>.Fail(ex.Message);
					break;
				case InvalidOperationException:
					code = HttpStatusCode.BadRequest;
					apiResponse = ApiResponse<object>.Fail(ex.Message);
					break;
				case DbUpdateException:
					code = HttpStatusCode.InternalServerError;
					apiResponse = ApiResponse<object>.Fail("Database update error");
					break;
				default:
					code = HttpStatusCode.InternalServerError;
					apiResponse = ApiResponse<object>.Fail("Internal server error");
					break;
			}
			_logger.LogError(ex, "Unhandled exception handled by middleware with status code {StatusCode}", (int)code);


			var json = JsonSerializer.Serialize(apiResponse, new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			});
			context.Response.ContentType = "application/json"; //đặt kiểu dữ liệu trả về
			context.Response.StatusCode = (int)code; //đặt mã trạng thái trả về

			return context.Response.WriteAsync(json); //trả về kết quả
		}
	}
}
