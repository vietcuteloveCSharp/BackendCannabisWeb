using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.DTO.Addresses
{
	public class AddressDTO
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string Country { get; set; } = string.Empty;

		public string Province { get; set; } = string.Empty;

		public string District { get; set; } = string.Empty;

		public string Commune { get; set; } = string.Empty;
		public string Road_Village_Hamlet { get; set; } = string.Empty;

		public string HouseNumber { get; set; } = string.Empty;

		public string PostalCode { get; set; } = string.Empty;

		public bool IsDefault { get; set; } = false;
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public DateTime UpdatedAt { get; set; }
	}
}
