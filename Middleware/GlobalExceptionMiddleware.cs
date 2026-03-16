using DTO.Response;
using Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;


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

		private static Task HandleExceptionAsync(HttpContext context, Exception ex)
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
			var logger = context.RequestServices.GetRequiredService<ILogger<GlobalExceptionMiddleware>>();
			logger.LogError(ex, "Unhandled exception handled by middleware with status code {StatusCode}", (int)code);

		
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
