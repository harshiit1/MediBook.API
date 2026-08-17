using Medicare.Application.Models.BackgroundJob.Appointment;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Interfaces.BackgroundJob.IAppointmentReminder
{
    public interface IAppointmentReminderRepository
    {
        Task<List<ReleaseableAppointmentModel>> GetReleasableAppointmentsAsync(Guid tenantId);
        Task<ResponseModel> ReleaseAppointmentSlotAsync(ReleaseAppointmentRequestModel model);
        Task<List<AppointmentBackgroundJobModel>> GetScheduledReminderListAsync(ScheduledReminderRequestModel model);
        Task<ResponseModel> LogReminderAsync(AppointmentReminderLogRequestModel model);
    }
}
