namespace Service.MapDTO_Entity
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
