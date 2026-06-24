using Shared.DTOs.Options;

namespace Cannabis.Server.Configuartions
{
	public  class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
	{
		private readonly JwtSettings _jwt;
		public ConfigureJwtBearerOptions(IOptions<JwtSettings> jwtOptions)
		{
			_jwt = jwtOptions.Value;
		}

		public void Configure(string? name, JwtBearerOptions options)
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = _jwt.Issuer,
				ValidAudience = _jwt.Audience,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key))
			};
		}

		public void Configure(JwtBearerOptions options) => Configure(Options.DefaultName, options);

	}
	
}
