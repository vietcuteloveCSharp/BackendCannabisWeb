namespace Service.Exceptions
{
	public class NotFoundException :Exception
	{
		public NotFoundException(string message) : base(message)
		{
		}
		public NotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}
		
		public NotFoundException(int id, string entityName) 
			: base($"The {entityName} with ID {id} was not found.")
		{
		}
	}
}
