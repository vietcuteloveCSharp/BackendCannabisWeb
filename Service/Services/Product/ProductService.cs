using AutoMapper.QueryableExtensions;
using DTO.DTOs.Products;
using Microsoft.EntityFrameworkCore;
using Service.IServices.Product;
using Service.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Product
{
	public class ProductService : BaseService<DAL.Entities.Product, ProductDTO, ProductCreateDTO,ProductUpdateDTO>,IProductService
	{
		

		public ProductService(IUnitOfWork unitOfWork, IMapper mapper) :base(unitOfWork, mapper) 
		{
			
			
		}
		
	}
}
