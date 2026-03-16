using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace Cannabis.Server.Extensions
{
	public class ValidateModelAttribute: ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid)
			{
				var errors = context.ModelState
					.Where(x => x.Value.Errors.Count > 0)
					.SelectMany(x => x.Value.Errors)
					.Select(e => e.ErrorMessage)
					.ToArray();

				throw new ValidationException(string.Join("; ", errors)); // ném exception
			}
		}
	}
}
