
namespace DAL.Repository.Interfaces.Internal
{
	public interface IStaffStatusRepository: IBaseRepository<StaffStatus>
	{
		Task<StaffStatus?> GetByCodeAsync(string code);
	}
}
