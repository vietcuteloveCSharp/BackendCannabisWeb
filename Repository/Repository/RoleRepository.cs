namespace Repository.Repository
{
	public class RoleRepository : BaseRepository<Role>, IRoleRepository
	{
		public RoleRepository(CannabisAccessorriesDBContext context) :base(context)
		{
			
		}
		
	}
}
