using Medicare.Application.Options;
using Medicare_API.Registrars;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Medicare.API.Registrars
{
    public class IdentityRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var jwtSettings = new JwtSettings();
            builder.Configuration.Bind(nameof(JwtSettings), jwtSettings);

            var jwtSection = builder.Configuration.GetSection(nameof(JwtSettings));
            builder.Services.Configure<JwtSettings>(jwtSection);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SigningKey)),
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };
                    options.Audience = jwtSettings.Audience;
                    options.ClaimsIssuer = jwtSettings.Issuer;
                });
        }
    }
}
