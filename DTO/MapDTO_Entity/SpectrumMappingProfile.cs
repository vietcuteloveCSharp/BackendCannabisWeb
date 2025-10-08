using DTO.DTOs.Spectrums;

namespace DTO.MapDTO_Entity
{
	public class SpectrumMappingProfile :Profile
	{
		public SpectrumMappingProfile()
		{
			#region Map Spectrum
			CreateMap<Spectrum, SpectrumDTO>(MemberList.None);
			CreateMap<SpectrumDTO, Spectrum>(MemberList.None);
			CreateMap<SpectrumCreateDTO, Spectrum>(MemberList.None)
			.ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
			
			CreateMap<SpectrumUpdateDTO, Spectrum>(MemberList.None);
			#endregion
		}
	}
}
