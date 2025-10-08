namespace DTO.DTOs.CarbonFilters
{
	public class CarbonFilterUpdateDTO 
	{

		
			[Required(ErrorMessage = "CarbonFilter name is required.")]
			[StringLength(255, ErrorMessage = "CarbonFilter name cannot exceed 255 characters.")]
			public string CarbonFilterName { get; set; } = string.Empty;

			[StringLength(150, ErrorMessage = "AirflowRate name cannot exceed 150 characters.")]
			public string AirflowRate { get; set; } = string.Empty;

			public int Quantity { get; set; } = 0;


			public decimal Price { get; set; }

			public string FilterMaterial { get; set; } = string.Empty;

			
			public decimal Diameter { get; set; }

			public decimal Length { get; set; }

			public int Lifespan { get; set; }

			public decimal MinTemperature { get; set; }

			public decimal MaxTemperature { get; set; }

			public string? Description { get; set; }

			public int WarrantyPeriod { get; set; }

			[StringLength(50, ErrorMessage = "ModelNumber name cannot exceed 50 characters.")]
			public string ModelNumber { get; set; } = string.Empty;
		
	}
}
