

namespace Shared.Common.Inherited
{
	public interface  ISoftDelete
	{
		public bool IsDeleted { get; set; } 
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }
	}
}
