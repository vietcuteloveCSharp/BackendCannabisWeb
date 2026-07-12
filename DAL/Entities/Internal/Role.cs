
namespace DAL.Entities.Internal
{
	public class Role :BaseEntity
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Role name is required.")]
		[StringLength(50, ErrorMessage = "Role name cannot exceed 50 characters.")]
		public string RoleName { get; set; } = string.Empty;

		[StringLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
		public string? Description { get; set; }

		// --- Navigation Properties ---
		// 🔒 Mối quan hệ 1-Nhiều: Một Quyền hạn gán cho nhiều Nhân viên/Admin nội bộ
		public virtual ICollection<Staff> Staffs { get; set; } = new HashSet<Staff>();
	}
}
