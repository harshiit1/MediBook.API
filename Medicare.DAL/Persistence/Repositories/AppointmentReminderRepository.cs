using Dapper;
using Medicare.Application.Interfaces.BackgroundJob.IAppointmentReminder;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Interfaces.INotificationRepository;
using Medicare.Application.Models.Appointment;
using Medicare.Application.Models.BackgroundJob.Appointment;
using Medicare.Application.Models.CommonModels.ErrorLog;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Notification;
using Medicare.DAL.Helper.Hubs;
using Medicare.DAL.Persistence.Dapper;
using Microsoft.AspNetCore.SignalR;

namespace Medicare.DAL.Persistence.Repositories
{
    public class AppointmentReminderRepository : IAppointmentReminderRepository
    {
        private readonly IDapperContext _context;
        private readonly IErrorLogRepository _errorLog;
        private readonly INotificationRepository _notifRepository;
        private readonly IHubContext<NotificationHub> _notifhub;
        public AppointmentReminderRepository(IDapperContext context, IErrorLogRepository errorLog, INotificationRepository notifRepository, IHubContext<NotificationHub> notifHub)
        {
            _context = context;
            _errorLog = errorLog;
            _notifRepository = notifRepository;
            _notifhub = notifHub;
        }
        public async Task<List<ReleaseableAppointmentModel>> GetReleasableAppointmentsAsync(Guid tenantId)
        {
            string procName = "USP_GetReleaseableAppointments";
            List<ReleaseableAppointmentModel> returnData = new List<ReleaseableAppointmentModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("TenantId", tenantId);

                returnData = await _context.QueryStoredProcListAsync<ReleaseableAppointmentModel>(procName, param);
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }

        public async Task<List<AppointmentBackgroundJobModel>> GetScheduledReminderListAsync(ScheduledReminderRequestModel model)
        {
            string procName = "USP_GetPendingReminders";
            List<AppointmentBackgroundJobModel> returnData = new List<AppointmentBackgroundJobModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("TenantId", model.TenantId);
                param.Add("ReminderType", model.ReminderType);
                
                returnData = await _context.QueryStoredProcListAsync<AppointmentBackgroundJobModel>(procName, param);
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }

        public async Task<ResponseModel> LogReminderAsync(AppointmentReminderLogRequestModel model)
        {
            string procName = "USP_LogAppointmentReminder";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("AppointmentId", model.AppointmentId);
                param.Add("ReminderType", model.ReminderType);
                param.Add("NotificationChannel", model.NotificationChannel);
                param.Add("Reminder24HrSent", model.Reminder24HrSent);
                param.Add("Reminder1WeekSent", model.Reminder1WeekSent);
                param.Add("SentTo", model.SentTo);

                returnData = await _context.QuerySingleStoredProcAsync<ResponseModel>(procName, param);
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }

        public async Task<ResponseModel> ReleaseAppointmentSlotAsync(ReleaseAppointmentRequestModel model)
        {
            string procName = "USP_ReleaseAppointmentSlot";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("AppointmentId", model.AppointmentId);
                param.Add("SlotId", model.SlotId);

                returnData = await _context.QuerySingleStoredProcAsync<ResponseModel>(procName, param);

                if (returnData.IsSuccess == 1 && returnData.ResponseId > 0)
                {
                    string appointmentProc = "USP_GetAppointmentById";
                    try
                    {
                        PatientAppointmentModel appointmentData = await _context.QuerySingleStoredProcAsync<PatientAppointmentModel>(appointmentProc, model.AppointmentId);

                        var title = "Appointment Auto-Cancelled";
                        var message = $"Your appointment with {appointmentData.DoctorName} at {appointmentData.SlotStartTime} has been Auto-Cancelled.";

                        await _notifRepository.CreateAsync(new SaveNotificationModel
                        {
                            RefId = appointmentData.PatientId,
                            UserType = "Patient",
                            Title = title,
                            Message = message,
                            NotifType = "AppointmentCreated",
                            ReferenceId = model.AppointmentId
                        });

                        await _notifhub.Clients
                                       .Group($"user-{appointmentData.PatientId}")
                                       .SendAsync("ReceiveNotification", new
                                       {
                                           RefId = appointmentData.PatientId,
                                           Message = message,
                                           Title = title,
                                           NotifType = "AppointmentCreated",
                                       });
                    }
                    catch (Exception ex)
                    {
                        await _errorLog.InsertErrorLog(new ErrorLogModel()
                        {
                            IsDBError = false,
                            Error_Message = ex.Message,
                            Error_Procedure = appointmentProc,
                            Error_Trace = ex.StackTrace
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }

            return returnData;
        }
    }
}
