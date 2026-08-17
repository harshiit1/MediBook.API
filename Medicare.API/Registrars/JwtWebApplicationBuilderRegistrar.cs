using Medicare.Application.Interfaces.JwtToken;
using Medicare.Application.Options;
using Medicare.DAL.Services;
using Medicare_API.Registrars;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;
using System.Net.WebSockets;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Medicare.API.Registrars
{
    public class JwtWebApplicationBuilderRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IJwtTokenRepository, JwtService>();

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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey)),
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,
                    };
                    options.Audience = jwtSettings.Audience;
                    options.ClaimsIssuer = jwtSettings.Issuer;

                    // For SignalR — WebSockets cannot send Authorization headers
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;

                            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs/notifications"))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
