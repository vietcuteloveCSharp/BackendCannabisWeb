using DTO.DTOs.Spectrums;

namespace DTO.MapDTO_Entity
{
	public class SpectrumMappingProfile :Profile
	{
		public SpectrumMappingProfile()
		{
			#region Map Spectrum
			CreateMap<Spectrum, SpectrumDTO>(MemberList.None).ReverseMap();
			CreateMap<SpectrumCreateDTO, Spectrum>(MemberList.None)
			.ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

			CreateMap<SpectrumUpdateDTO, Spectrum>(MemberList.None)
			.ForMember(dest => dest.SpectrumChartUrl, opt => opt.Ignore()); // THÊM DÒNG NÀY
			#endregion
		}
	}
}
