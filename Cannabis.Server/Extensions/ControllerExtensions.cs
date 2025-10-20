namespace Cannabis.Server.Extensions
{
	public static class ControllerExtensions
	{
		public static IActionResult ValidateModelState(this ControllerBase controller)
		{
			var errors = controller.ModelState
				.Where(kvp => kvp.Value?.Errors.Count > 0)
				.ToDictionary(
					kvp => kvp.Key,
					kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
				);

			var response = new ApiResponse<Dictionary<string, string[]>>
			{
				Success = false,
				Message = "Validation failed",
				Data = errors
			};

			return controller.BadRequest(response);
		}
	}
}
