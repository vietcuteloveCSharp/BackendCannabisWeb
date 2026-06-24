namespace DAL.Entities.User
{
	public class UserStatus
	{
		public int Id { get; set; } // khóa chính
		public string Code { get; set; } = default!;
		public string Name { get; set; } = default!;

		//Navigation
		public ICollection<User> Users { get; set; } = new HashSet<User>();

	}
}
