using DTO.DTOs.ChipModels;
using DTO.DTOs.CoolingSystems;
using DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices.Inventory
{
	public interface ICoolingSystemService : IBaseService<CoolingSystem, CoolingSystemDTO,CoolingSystemCreateDTO,CoolingSystemUpdateDTO>
	{
		
	}
}
