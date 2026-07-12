namespace DAL.Entities.Internal
{
	public class StaffStatus
	{
		public int Id { get; set; } // khóa chính
		public string Code { get; set; } = default!;
		public string Name { get; set; } = default!;

		//Navigation
		public ICollection<Staff> Staffs { get; set; } = new HashSet<Staff>();

	}
}
