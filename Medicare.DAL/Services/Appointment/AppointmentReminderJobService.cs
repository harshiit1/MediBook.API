using Medicare.Application.Interfaces.BackgroundJob.IAppointmentReminder;
using Medicare.Application.Interfaces.IEmail;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Models.BackgroundJob.Appointment;
using Medicare.Application.Models.CommonModels.Email;
using Medicare.Application.Models.CommonModels.ErrorLog;

namespace Medicare.DAL.Services.Appointment
{
    public class AppointmentReminderJobService
    {
        private readonly IAppointmentReminderRepository _reminderRepository;
        private readonly IEmailService _emailService;
        private readonly IErrorLogRepository _errorLog;
        public AppointmentReminderJobService(
            IAppointmentReminderRepository reminderRepository,
            IEmailService emailService,
            IErrorLogRepository errorLog)
        {
            _reminderRepository = reminderRepository;
            _emailService = emailService;
            _errorLog = errorLog;
        }
        public async Task ProcessScheduledRemindersAsync(Guid tenantId)
        {
            // 24hr reminder 
            var appointments24Hr = await _reminderRepository.GetScheduledReminderListAsync(
                new ScheduledReminderRequestModel
                {
                    TenantId = tenantId,
                    ReminderType = "24Hr"
                });

            if (appointments24Hr?.Any() == true)
                foreach (var appt in appointments24Hr)
                    await SendScheduledReminderAsync(appt, "24Hr");

            // 1-Week reminder 
            var appointments1Week = await _reminderRepository.GetScheduledReminderListAsync(
                new ScheduledReminderRequestModel
                {
                    TenantId = tenantId,
                    ReminderType = "1Week"
                });

            if (appointments1Week?.Any() == true)
                foreach (var appt in appointments1Week)
                    await SendScheduledReminderAsync(appt, "1Week");
        }

        private async Task SendScheduledReminderAsync(AppointmentBackgroundJobModel appointment, string reminderType)
        {
            bool success = false;
            try
            {
                success = appointment.NotificationChannel switch
                {
                    "EMAIL" => await SendScheduledReminderEmailAsync(appointment, reminderType),
                    //"SMS" => await SendSmsReminderAsync(appointment),
                    //"WHATSAPP" => await SendWhatsAppReminderAsync(appointment),
                    _ => await SendScheduledReminderEmailAsync(appointment, reminderType)
                };
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel
                {
                    IsDBError = false,
                    Error_Message = $"{ex.Message}, {reminderType} Reminder | AppointmentId: {appointment.AppointmentId})",
                    Error_Procedure = "",
                    Error_Trace = ex.StackTrace
                }); ;
            }
            
            // Log to AppointmentReminderLog — prevents duplicate sends
            await _reminderRepository.LogReminderAsync(new AppointmentReminderLogRequestModel
            {
                AppointmentId = appointment.AppointmentId,
                ReminderType = reminderType,
                NotificationChannel = appointment.NotificationChannel,
                SentTo = appointment.PatientEmail,  
                Reminder24HrSent = reminderType == "24Hr" ? 1 : 0,
                Reminder1WeekSent = reminderType == "1Week" ? 1 : 0
            });
        }

        private async Task<bool> SendScheduledReminderEmailAsync(AppointmentBackgroundJobModel model, string reminderType)
        {
            bool is24Hr = reminderType == "24Hr";
            var headerBg = is24Hr ? "#0066cc" : "#1a8a1a";
            var headerText = is24Hr ? "Your appointment is tomorrow" : "Your appointment is in 7 days";
            var subject = is24Hr
                ? $"Reminder: Your appointment tomorrow with {model.DoctorName}"
                : $"Reminder: Your appointment next week with {model.DoctorName}";

            var body = $"""
                <div style="font-family:Arial,sans-serif;max-width:600px;margin:auto;border:1px solid #e0e0e0;border-radius:8px;overflow:hidden;">
                  <div style="background:{headerBg};padding:24px;text-align:center;">
                    <h1 style="color:white;margin:0;">Appointment Reminder</h1>
                    <p style="color:#cce5ff;margin:8px 0 0;">{headerText}</p>
                  </div>
                  <div style="padding:24px;">
                    <p>Hi <strong>{model.PatientName}</strong>,</p>
                    <p>This is a reminder for your upcoming appointment.</p>
                    <table style="width:100%;border-collapse:collapse;margin:16px 0;">
                      <tr style="background:#f5f5f5;">
                        <td style="padding:10px;font-weight:bold;width:40%;">Doctor</td>
                        <td style="padding:10px;">{model.DoctorName}</td>
                      </tr>
                      <tr>
                        <td style="padding:10px;font-weight:bold;">Hospital</td>
                        <td style="padding:10px;">{model.HospitalName}</td>
                      </tr>
                      <tr>
                        <td style="padding:10px;font-weight:bold;">Date</td>
                        <td style="padding:10px;">{model.AppointmentDate}</td>
                      </tr>
                      <tr style="background:#f5f5f5;">
                        <td style="padding:10px;font-weight:bold;">Appointment At</td>
                        <td style="padding:10px;">{model.SlotStartTime}</td>
                      </tr>
                      <tr>
                        <td style="padding:10px;font-weight:bold;">Visit Type</td>
                        <td style="padding:10px;">{model.VisitType}</td>
                      </tr>
                    </table>
                    <p style="color:#666;font-size:13px;">
                      Please arrive 10 minutes early. If you need to cancel or reschedule,
                      please do so at least 24 hours before your appointment to avoid any cancellation fees.
                    </p>
                  </div>
                  <div style="background:#f5f5f5;padding:16px;text-align:center;">
                    <p style="color:#999;font-size:12px;margin:0;">MediBook — Your Health, Our Priority</p>
                  </div>
                </div>
                """;

            return await _emailService.SendEmailAsync(new EmailModel
            {
                ToEmail = model.PatientEmail,
                ToName = model.PatientName,
                Subject = subject,
                Body = body,
                IsHtml = true
            });
        }

        private Task<bool> SendSmsReminderAsync(AppointmentBackgroundJobModel a)
        {
            // Integrate SMS provider (Twilio / AWS SNS / MSG91)
            // var message = $"Hi {a.PatientName}, your appointment with {a.DoctorName} on {a.AppointmentDate} at {a.SlotStartTime} is pending confirmation. Please verify your OTP to confirm.";
            // return await _smsService.SendAsync(a.PatientPhone, message);
            return Task.FromResult(false);
        }
        private Task<bool> SendWhatsAppReminderAsync(AppointmentBackgroundJobModel a)
        {
            // Integrate WhatsApp provider (Twilio / Meta Cloud API)
            return Task.FromResult(false);
        }
    }
}
