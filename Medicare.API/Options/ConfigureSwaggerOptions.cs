using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Medicare_API.Options
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName, 
                    CreateVersionInfo(description)
                );
            }

            var scheme = GetJwtSecurityScheme();
            options.AddSecurityDefinition("Bearer", scheme);

            options.AddSecurityRequirement(options => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", options)] = []
            });
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = "Medicare",
                Version = description.ApiVersion.ToString()
            };

            if (description.IsDeprecated)
            {
                info.Title = "This API Version has been Deprecated";
            }

            return info;    
        }

        private OpenApiSecurityScheme GetJwtSecurityScheme()
        {
            return new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "Authorization",
                Description = "Enter your JWT token in the format: Bearer {your_token}",
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Type = SecuritySchemeType.Http,
                In = ParameterLocation.Header
            };
        }
    }
}
