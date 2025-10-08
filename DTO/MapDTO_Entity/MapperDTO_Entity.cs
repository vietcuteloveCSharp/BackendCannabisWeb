namespace DTO.MapDTO_Entity
{
	public class MapperDTO_Entity :Profile
	{
		public MapperDTO_Entity()
		{
			#region Map User
			CreateMap<CreateUserDTO, User>(MemberList.None)
				.ForMember(dest => dest.HashPassword, opt => opt.Ignore());
			CreateMap<User, UserDTO>(MemberList.None)
				.ForMember(dest => dest.Password, opt => opt.Ignore());
			CreateMap<User, UserSummaryDTO>(MemberList.None)
			  .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role!=null ?src.Role.RoleName.ToString():null));
			#endregion
			#region Map Role
			CreateMap<Role, RoleDTO>(MemberList.None);
			CreateMap<RoleDTO, Role>(MemberList.None);
			CreateMap<CreateRoleDTO, Role>(MemberList.None);
			CreateMap<RoleUpdateDTO, Role>(MemberList.None);
			#endregion
			#region Map Brand
			CreateMap<Brand, BrandDTO>(MemberList.None);
			CreateMap<BrandCreateDTO, Brand>(MemberList.None);
			CreateMap<BrandDTO, Brand>(MemberList.None);
			CreateMap<BrandUpdateDTO, Brand>(MemberList.None);
			#endregion
			#region Map Spectrum
			CreateMap<Spectrum,SpectrumDTO>(MemberList.None);
			CreateMap<SpectrumDTO, Spectrum>(MemberList.None);
			CreateMap<SpectrumCreateDTO, Spectrum>(MemberList.None);
			CreateMap<SpectrumUpdateDTO, Spectrum>(MemberList.None);
			#endregion
			#region Map PowerSupply
			CreateMap<PowerSupply, PowerSupplyDTO>(MemberList.None);
			CreateMap<PowerSupplyDTO, PowerSupply>(MemberList.None);
			CreateMap<PowerSupplyCreateDTO, PowerSupply>(MemberList.None);
			CreateMap<PowerSupplyUpdateDTO, PowerSupply>(MemberList.None);
			#endregion
			#region Map CarbonFiler
			CreateMap<CarbonFilter, CarbonFilterDTO>(MemberList.None);
			CreateMap<CarbonFilterDTO, CarbonFilter>(MemberList.None);
			CreateMap<CarbonFilter, CarbonFilterCreateDTO>(MemberList.None);
			CreateMap<CarbonFilterCreateDTO, CarbonFilter>(MemberList.None);
			CreateMap<CarbonFilter, CarbonFilterUpdateDTO>(MemberList.None);
			CreateMap<CarbonFilterUpdateDTO, CarbonFilter>(MemberList.None);
			#endregion
			#region Map RefreshToken
			CreateMap<RefreshToken, RefreshTokenDTO>(MemberList.None);
			CreateMap<RefreshTokenDTO, RefreshToken>(MemberList.None);
			CreateMap<RefreshTokenCreateDTO, RefreshToken>(MemberList.None);
			#endregion
			#region Map Classification
			CreateMap<Classification, ClassificationDTO>(MemberList.None);
			CreateMap<ClassificationDTO, Classification>(MemberList.None);
			CreateMap<CreateClassificationDTO, Classification>(MemberList.None);
			CreateMap<UpdateClassificationDTO, UpdateClassificationDTO>(MemberList.None);
			#endregion
			#region Map GrowTent
			CreateMap<GrowTent, GrowTentDTO>(MemberList.None);
			CreateMap<GrowTentDTO, GrowTent>(MemberList.None);
			CreateMap<GrowTentCreateDTO, GrowTent>(MemberList.None);
			CreateMap<GrowTentUpdateDTO, GrowTent>(MemberList.None);
			#endregion
			#region Map NutrientType
			CreateMap<NutrientType, NutrientTypeDTO>(MemberList.None);
			CreateMap<NutrientTypeDTO, NutrientType>(MemberList.None);
			CreateMap<NutrientTypeCreateDTO, NutrientType>(MemberList.None);
			CreateMap<NutrientTypeUpdateDTO, NutrientType>(MemberList.None);
			#endregion
			#region Map Nutrient
			CreateMap<NutrientCreateDTO, Nutrient>();
			CreateMap<NutrientUpdateDTO, Nutrient>();
			CreateMap<Nutrient, NutrientDTO>()
				.ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand != null ? src.Brand.BrandName : null))
				.ForMember(dest => dest.NutrientTypeName, opt => opt.MapFrom(src => src.NutrientType != null ? src.NutrientType.NutrientName : null));
			#endregion

		}
	}
}
