using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Enum.EnumableClass.EnumableClass;

namespace DTO.DTOs.Spectrums
{
	public class SpectrumUpdateDTO
	{
		[Column(TypeName = "nvarchar(20)")]
		public ESpectrumType Type { get; set; }
		public string? Description { get; set; }
	}
}
