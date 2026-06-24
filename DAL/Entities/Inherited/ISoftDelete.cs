using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Inherited
{
	public interface  ISoftDelete
	{
		public bool IsDeleted { get; set; } 
		public DateTime? DeletedAt { get; set; }
		public int? DeletedBy { get; set; }
	}
}
