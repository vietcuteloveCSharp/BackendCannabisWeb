using DTO.DTOs.CoolingSystems;
using DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices.Inventory
{
	public interface ICoolingSystemService
	{
		Task<IEnumerable<CoolingSystemDTO>> GetAllActiveAsync();
		Task<IEnumerable<CoolingSystemDTO>> GetAllAsync();
		Task<CoolingSystemDTO?> GetByIdAsync(int id);
		Task<CoolingSystemDTO> CreateAsync(CoolingSystemCreateDTO dto);
		Task<bool> UpdateAsync(int id, CoolingSystemUpdateDTO dto);
		Task<bool> DeleteAsync(int id);
		Task<bool> ExistsAsync(int id);
	}
}
