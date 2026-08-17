using Medicare.DAL.Helper.Hubs;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Medicare_API.Registrars
{
    public class MVCWebAppRegistrar : IWebApplicationRegistrar
    {
        public void RegistrarPipelineComponents(WebApplication app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.ApiVersion.ToString()
                       );
                }
                options.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseCors("MyPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapHub<NotificationHub>("/hubs/notifications");

            app.MapControllers();
        }
    }
}
