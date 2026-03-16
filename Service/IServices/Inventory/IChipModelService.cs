using DTO.DTOs.ChipModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices.Inventory
{
	public interface IChipModelService
	{
		Task<IEnumerable<ChipModelDTO>> GetAllAsync();
		Task<IEnumerable<ChipModelDTO>> GetAllActiveAsync();
		Task<ChipModelDTO?> GetByIdAsync(int id);
		Task<ChipModelDTO> CreateAsync(ChipModelCreateDTO dto);
		Task<bool> UpdateAsync(int id, ChipModelUpdateDTO dto);
		Task<bool> DeleteAsync(int id);
		Task<bool> ExistAsync(int id);
		
	}
}
