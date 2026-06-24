namespace DAL.Repository.Interfaces
{
	public interface IUserStatusRepository :IBaseRepository<UserStatus>
	{
		Task<UserStatus?> GetCodeAsync(string Code);
	}
}
