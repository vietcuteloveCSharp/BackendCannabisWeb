using DTO.DTOs.Breeders;
using System.Threading.Tasks;

namespace Service.IServices.Product
{
	public interface IBreederService
	{
		Task<IEnumerable<BreederDTO>> GetAllBreedersAsync();
		Task<BreederDTO?> GetBreederByIdAsync(int id);
		Task<BreederDTO?> GetBreederByNameAsync(string breederName);

		Task<BreederDTO?> AddBreederAsync(BreederCreateDTO breederCreateDTO);

		Task<bool> UpdateBreederAsync(int id, BreederUpdateDTO breederUpdateDTO);
		Task<bool> BreederNameExistsAsync(string breederName);
	}
}
