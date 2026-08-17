using Medicare.Application.Interfaces.Dapper;
using Medicare.DAL.Persistence.Dapper;
using Medicare_API.Registrars;
using Microsoft.EntityFrameworkCore;

namespace Medicare.API.Registrars
{
    public class DBRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var cs = builder.Configuration.GetConnectionString("Default");

            builder.Services.AddSingleton<IDbConnectionFactory>(sp => new DapperConnectionFactory(cs));

            builder.Services.AddSingleton<IDapperContext, DapperContext>();
        }
    }
}
