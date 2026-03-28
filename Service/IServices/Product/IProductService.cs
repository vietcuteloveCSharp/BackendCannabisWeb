using DTO.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices.Product
{
	public interface IProductService : IBaseService<DAL.Entities.Product,ProductDTO,ProductCreateDTO,ProductUpdateDTO>
	{
		
	}
}
