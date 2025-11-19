using DTO.DTOs.Categories;

namespace DTO.MapDTO_Entity
{
	public class CategoryMappingProfile :Profile
	{
		public CategoryMappingProfile()
		{
			CreateMap<CategoryCreateDTO, Category>();
			CreateMap<CategoryUpdateDTO, Category>();
			CreateMap<Category, CategoryDTO>().ReverseMap();
		}
		
	}
}
