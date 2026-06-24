
using DAL.Entities.Inherited;

namespace DAL.Entities.Inventory
{
	public class Warehouse : BaseEntity, ISoftDelete
	{
		public int Id { get; set; }
		public string Name { get; set; }= string.Empty;
		public string Address { get; set; } = string.Empty;	
		public bool IsDeleted { get ; set ; }
		public DateTime? DeletedAt { get ; set ; }
		public int? DeletedBy { get; set; }
		public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
	}
}
