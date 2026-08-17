using Medicare.Application.Features.Commands.Authentication;
using Medicare.Application.Interfaces.BackgroundJob.IAppointmentReminder;
using Medicare.Application.Interfaces.IAppointment;
using Medicare.Application.Interfaces.IAssociate;
using Medicare.Application.Interfaces.IAuthRepository;
using Medicare.Application.Interfaces.IBilling;
using Medicare.Application.Interfaces.IDoctor;
using Medicare.Application.Interfaces.IEmail;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Interfaces.ILab;
using Medicare.Application.Interfaces.ILocations;
using Medicare.Application.Interfaces.IMessage;
using Medicare.Application.Interfaces.INotificationRepository;
using Medicare.Application.Interfaces.IOrders;
using Medicare.Application.Interfaces.IPatient;
using Medicare.Application.Interfaces.ISecurityQuestionsRepository;
using Medicare.Application.Interfaces.JwtToken;
using Medicare.Application.Interfaces.Master;
using Medicare.Application.Interfaces.UserRepository;
using Medicare.Application.Models.CommonModels.Email;
using Medicare.DAL.Persistence.Repositories;
using Medicare.DAL.Services;
using Medicare.DAL.Services.Appointment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Medicare_API.Registrars
{
    public class MVCRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {

            builder.Services.AddControllers();

            builder.Services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            builder.Services.AddVersionedApiExplorer(config =>
            {
                config.GroupNameFormat = "'v'VVV";
                config.SubstituteApiVersionInUrl = true;
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(UserCommand).Assembly)
            );

            builder.Services.Configure<EmailSettingsModel>(
                builder.Configuration.GetSection("EmailSettings")
            );

            // ✅ Repository
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<ISecurityQuestionRepository, SecurityQuestionRepository>();
            builder.Services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>(); 
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            builder.Services.AddScoped<ILocationRepository, LocationRepository>();
            builder.Services.AddScoped<IRxOrderRepository, RxOrderRepository>();
            builder.Services.AddScoped<IAssociateRepository, AssociateRepository>();
            builder.Services.AddScoped<IMasterRepository, MasterRepository>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddScoped<IBillingRepository, BillingRepository>();
            builder.Services.AddScoped<ILabRepository, LabRepository>();

            // ✅ Services
            builder.Services.AddSignalR();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IEmailJobService, EmailJobService>();
            builder.Services.AddScoped<IJwtTokenRepository, JwtService>();
            builder.Services.AddScoped<IAppointmentReminderRepository, AppointmentReminderRepository>();

            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<AppointmentReminderJobService>();
            builder.Services.AddScoped<AppointmentSlotReleaseJobService>();

            // ✅ Helper
            builder.Services.AddSingleton<PasswordHelper>();
        }
    }
}
    