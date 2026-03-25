using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOs.Spectrums
{
	public class SpectrumDTO
	{
		public int SpectrumId { get; set; }
		public ESpectrumType Type { get; set; }
		public string? Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
