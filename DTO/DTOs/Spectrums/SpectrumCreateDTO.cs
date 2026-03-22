using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOs.Spectrums
{
	public class SpectrumCreateDTO
	{
		[Required(ErrorMessage = "Type is required.")]
		[Column(TypeName = "nvarchar(20)")]
		public ESpectrumType Type { get; set; }
		public string? Description { get; set; }
	}
}
