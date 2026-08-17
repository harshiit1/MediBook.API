using Medicare_API.Registrars;

namespace Medicare.API.Registrars
{
    public class CorsRegistrar :IWebApplicationBuilderRegistrar 
    {
        private const string PolicyName = "MyPolicy";

        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(PolicyName, policy =>
                {
                    policy
                        .WithOrigins(
                            builder.Configuration
                                   .GetSection("Cors:AllowedOrigins")
                                   .Get<string[]>()
                            ?? Array.Empty<string>()
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }
    }
}
