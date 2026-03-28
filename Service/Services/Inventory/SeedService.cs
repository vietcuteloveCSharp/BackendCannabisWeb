using AutoMapper.QueryableExtensions;
using DTO.DTOs.Seeds;
using DTO.Response;
using Microsoft.EntityFrameworkCore;
using Service.IServices.Inventory;
using Service.Services.BaseService;

namespace Service.Services.Inventory
{
	public class SeedService : BaseService<Seed, SeedDTO, SeedCreateRequestDTO, SeedUpdateDTO>, ISeedService
	{
		
		
		public SeedService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) 
		{
			
		}
		
		
	}
}
