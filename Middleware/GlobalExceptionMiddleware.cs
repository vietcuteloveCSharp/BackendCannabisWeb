using Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Middleware
{
	public class GlobalExceptionMiddleware
	{	private readonly ILogger<GlobalExceptionMiddleware> _logger;
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

		private static Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			//phân loại lỗi
			var code = HttpStatusCode.InternalServerError;
			switch(ex)
			{
				case KeyNotFoundException:
					code = HttpStatusCode.NotFound;
					break;
				case NotFoundException:
					code = HttpStatusCode.NotFound;
					break;
				case UnauthorizedAccessException:
					code = HttpStatusCode.Unauthorized;
					break;
				case ArgumentException:
					code = HttpStatusCode.BadRequest;
					break;
				case InvalidOperationException:
					code = HttpStatusCode.BadRequest;
					break;
				case DbUpdateException:
					code = HttpStatusCode.InternalServerError;
					break;
			}
			var logger = context.RequestServices.GetRequiredService<ILogger<GlobalExceptionMiddleware>>();
			logger.LogError(ex, "Unhandled exception handled by middleware with status code {StatusCode}", (int)code);
			var result = JsonSerializer.Serialize(new
			{	
				success=false,
				status=(int)code,
				error = ex.Message,
				detail = ex.InnerException?.Message
			});
			context.Response.ContentType = "application/json"; //đặt kiểu dữ liệu trả về
			context.Response.StatusCode = (int)code; //đặt mã trạng thái trả về
			return context.Response.WriteAsync(result); //trả về kết quả
		}
	}
}
