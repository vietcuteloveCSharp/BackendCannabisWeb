namespace Cannabis.Server.DependencyInjection
{
	public static class ApiResponseExtendsion
	{
		public static IServiceCollection AddConfigApiResponse(this IServiceCollection service)
		{
			service.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = context =>
				{
					var errors = context.ModelState.Values
						.SelectMany(v => v.Errors)
						.Select(e => e.ErrorMessage)
						.ToList();

					var response = ApiResponse<object>.Fail("Validation failed", errors);
					return new BadRequestObjectResult(response);
				};
			});
			return service;
		}
	}
}
