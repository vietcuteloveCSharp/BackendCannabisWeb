using DTO.DTOs.Breeders;
using System.Threading.Tasks;

namespace Service.IServices.Product
{
	public interface IBreederService
	{
		Task<IEnumerable<BreederDTO>> GetAllAsync();
		Task<IEnumerable<BreederDTO>> GetAllActiveAsync();
		Task<BreederDTO?> GetByIdAsync(int id);
		Task<BreederDTO?> GetByNameAsync(string breederName);

		Task<BreederDTO?> AddAsync(BreederCreateDTO breederCreateDTO);

		Task<bool> UpdateAsync(int id, BreederUpdateDTO breederUpdateDTO);
		Task<bool> DeleteAsync(int id);
		Task<bool> NameExistsAsync(string breederName);
		Task<bool> ExistAsync(int id);
	}
}
