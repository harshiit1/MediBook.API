using Medicare.Application.Interfaces.BackgroundJob.IAppointmentReminder;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Models.BackgroundJob.Appointment;
using Medicare.Application.Models.CommonModels.ErrorLog;

namespace Medicare.DAL.Services.Appointment
{
    public class AppointmentSlotReleaseJobService
    {
        private readonly IAppointmentReminderRepository _reminderRepository;

        private readonly IErrorLogRepository _errorLog;

        public AppointmentSlotReleaseJobService(IAppointmentReminderRepository reminderRepository, IErrorLogRepository errorLog)
        {
            _reminderRepository = reminderRepository;
            _errorLog = errorLog;
        }

        // Processes every 60 mins 
        public async Task ReleaseExpiredSlotsAsync(Guid tenantId)
        {
            var releasable = await _reminderRepository.GetReleasableAppointmentsAsync(tenantId);

            if (releasable == null || !releasable.Any()) return;

            foreach (var appointment in releasable)
            {
                try
                {
                    var releaseAppointmentModel = new ReleaseAppointmentRequestModel
                    {
                        AppointmentId = appointment.AppointmentId,
                        SlotId = appointment.SlotId,
                    };

                    await _reminderRepository.ReleaseAppointmentSlotAsync(releaseAppointmentModel);
                }
                catch (Exception ex)
                {
                    await _errorLog.InsertErrorLog(new ErrorLogModel
                    {
                        IsDBError = false,
                        Error_Message = $"{ex.Message}; AppointmentId: {appointment.AppointmentId}",
                        Error_Procedure = "",
                        Error_Trace = ex.StackTrace
                    });
                }
            }
        }
    }
}
