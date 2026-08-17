using Hangfire;
using Medicare.API.Options;
using Medicare.Application.Models.Hospital;
using Medicare.DAL.Persistence.Dapper;
using Medicare.DAL.Services.Appointment;
using Medicare_API.Registrars;

namespace Medicare.API.Registrars
{
    public class HangfireWebAppRegistrar : IWebApplicationRegistrar
    {
        public void RegistrarPipelineComponents(WebApplication app)
        {
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[]
               {
                   new ConfigureHangfireAuthOptions(
                       app.Services.GetRequiredService<IWebHostEnvironment>())
               }
            });

            //Scheduling Jobs per Tenant
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<IDapperContext>();

            var logger = app.Services.GetRequiredService<ILogger<HangfireRegistrar>>();

            var tenants = db.QueryAsync<HospitalTenantModel>(
                 "SELECT HospitalId, TenantId FROM dbo.HospitalMaster WHERE IsActive = 1")
                .GetAwaiter()
                .GetResult();

            logger.LogInformation("Total tenants found: {Count}", tenants?.Count() ?? 0);

            if (tenants == null || !tenants.Any())
            {
                logger.LogWarning("No active tenants found — Hangfire jobs not registered.");
                return;
            }

            foreach (var tenant in tenants)
            {
                logger.LogInformation("Registering jobs for TenantId: {TenantId}, HospitalId: {HospitalId}",
                    tenant.TenantId, tenant.HospitalId);

                var tenantId = tenant.TenantId;

                // Reminder job — every 30 mins per tenant
                RecurringJob.AddOrUpdate<AppointmentReminderJobService>(
                    $"appointment-reminder-{tenantId}",
                    job => job.ProcessScheduledRemindersAsync(tenantId),
                    "*/30 * * * *"
                );

                // Slot release job — every 60 mins per tenant
                RecurringJob.AddOrUpdate<AppointmentSlotReleaseJobService>(
                    $"slot-release-{tenantId}",
                    job => job.ReleaseExpiredSlotsAsync(tenantId),
                    "0 * * * *"
                );
            }
        }
    }
}
