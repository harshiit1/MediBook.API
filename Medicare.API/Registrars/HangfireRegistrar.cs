using Hangfire;
using Hangfire.SqlServer;
using Medicare_API.Registrars;

namespace Medicare.API.Registrars
{
    public class HangfireRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var conn = builder.Configuration.GetConnectionString("Default");

            builder.Services.AddHangfire(config =>
            {
                config
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(conn, new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    });
            });

            builder.Services.AddHangfireServer(options =>
            {
                options.WorkerCount = 2;      
                options.Queues = new[] { "emails", "default" };
            });
        }
    }
}
