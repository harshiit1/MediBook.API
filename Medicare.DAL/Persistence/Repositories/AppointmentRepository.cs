using Dapper;
using Medicare.Application.Interfaces.IAppointment;
using Medicare.Application.Interfaces.IEmail;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Interfaces.INotificationRepository;
using Medicare.Application.Interfaces.JwtToken;
using Medicare.Application.Models.Appointment;
using Medicare.Application.Models.CommonModels.Email;
using Medicare.Application.Models.CommonModels.ErrorLog;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Notification;
using Medicare.Application.Models.Patient;
using Medicare.DAL.Helper.Hubs;
using Medicare.DAL.Persistence.Dapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Medicare.DAL.Persistence.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IDapperContext _context;
        private readonly IErrorLogRepository _errorLog;
        private readonly INotificationRepository _notifRepository;
        private readonly IHubContext<NotificationHub> _notifhub;
        private readonly IEmailService _emailService;
        private readonly IJwtTokenRepository _tokenRepository;
        private readonly string _baseUrl;
        private readonly IConfiguration _config;
        public AppointmentRepository(
            IDapperContext context, IErrorLogRepository errorLog, 
            INotificationRepository notifRepository, IHubContext<NotificationHub> notifHub, 
            IEmailService emailService, IJwtTokenRepository tokenRepository,
            IConfiguration config)
        {
            _context = context;
            _errorLog = errorLog;
            _notifRepository = notifRepository;
            _notifhub = notifHub;
            _emailService = emailService;
            _tokenRepository = tokenRepository;
            _config = config;
            _baseUrl = config["AppSettings:BaseUrl"] ?? string.Empty;
        }

        public async Task<List<PatientAppointmentModel>> GetMyAppointmentListByPatientIdAsync(int patientId)
        {
            string procName = "USP_GetMyAppointmentListByPatientId";
            List<PatientAppointmentModel> returnData = new List<PatientAppointmentModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("PatientId", patientId);

                returnData = await _context.QueryStoredProcListAsync<PatientAppointmentModel>(procName, param);
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

        public async Task<List<AvailableAppointmentModel>> GetAvailableAppointmentsAsync(int associateId)
        {
            string procName = "USP_GetAvailableAppointments";
            List<AvailableAppointmentModel> returnData = new List<AvailableAppointmentModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("AssociateId", associateId);

                returnData = await _context.QueryStoredProcListAsync<AvailableAppointmentModel>(procName, param);
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

        public async Task<AppointmentDetailModelDto> GetAppointmentById(int appointmentId)
        {
            string procName = "USP_GetAppointmentById";
            AppointmentDetailModelDto returnData = new AppointmentDetailModelDto();
            try
            {
                var param = new DynamicParameters();

                param.Add("AppointmentId", appointmentId);

                returnData = await _context.QuerySingleStoredProcAsync<AppointmentDetailModelDto>(procName, param);
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

        public async Task<ResponseModel> CreateAppointmentAsync(AppointmentMasterModel model)
        {
            string profileProc = "USP_AddPatientProfile";
            string appointmentProc = "USP_CreateAppointment";
            ResponseModel returnData = new ResponseModel();

            try
            {
                if (model.RelatonTypeId != null && model.RelatonTypeId != 1)
                {
                    var profileParam = new DynamicParameters();
                    profileParam.Add("PatientId", model.PatientId);
                    profileParam.Add("FirstName", model.FirstName);
                    profileParam.Add("LastName", model.LastName);
                    profileParam.Add("DateOfBirth", model.DateOfBirth);
                    profileParam.Add("Age", model.Age);
                    profileParam.Add("AgeTypeId", model.AgeTypeId);
                    profileParam.Add("Gender", model.Gender);
                    profileParam.Add("Email", model.Email);
                    profileParam.Add("PhoneNumber", model.Phone);
                    profileParam.Add("RelationTypeId", model.RelatonTypeId);

                    var profileResult = await _context.QuerySingleStoredProcAsync<ResponseModel>(profileProc, profileParam);

                    if (profileResult.IsSuccess == 0) return profileResult;

                    model.ProfileId = profileResult.ResponseId;
                }

                var param = new DynamicParameters();
                param.Add("PatientId", model.PatientId);
                param.Add("ProfileId", model.ProfileId);   
                param.Add("AssociateId", model.AssociateId);
                param.Add("SlotId", model.SlotId);
                param.Add("VisitPurpose", model.VisitPurpose);
                param.Add("VisitType", model.VisitType);
                param.Add("OtpMethod", model.OtpMethod);
                param.Add("CreatedBy", model.CreatedBy);
                param.Add("AssociateRole", model.AssociateRole);

                param.Add("Insurance", model.Insurance);
                param.Add("Provider", model.InsuranceData?.Provider);
                param.Add("Policy", model.InsuranceData?.Policy);
                param.Add("GroupId", model.InsuranceData?.GroupId);
                param.Add("HolderName", model.InsuranceData?.HolderName);
                param.Add("Address", model.InsuranceData?.Address);

                param.Add("PaymentType", model.PaymentData?.PaymentType);
                param.Add("CardHolder", model.PaymentData?.CardHolder);
                param.Add("CardNumber", model.PaymentData?.CardNumber);
                param.Add("Expiry", model.PaymentData?.Expiry);
                param.Add("CvvHash", model.PaymentData?.CvvHash, dbType: DbType.Binary);
                param.Add("CvvSalt", model.PaymentData?.CvvSalt, dbType: DbType.Binary);

                returnData = await _context.QuerySingleStoredProcAsync<ResponseModel>(appointmentProc, param);

                if(returnData.IsSuccess == 1 && returnData.ResponseId > 0)
                {
                    try
                    {
                        var title = "Appointment Created";
                        var message = "Your Appointment has been Scheduled. A Confirmation Mail will be Shared Soon.";

                        await _notifRepository.CreateAsync(new SaveNotificationModel
                        {
                            RefId = model.PatientId,
                            UserType = "Patient",
                            Title = title,
                            Message = message,
                            NotifType = "AppointmentCreated",
                            ReferenceId = returnData.ResponseId
                        });

                        await _notifhub.Clients
                            .Group($"user-{model.PatientId}")
                            .SendAsync("ReceiveNotification", new
                            {
                                RefId = model.PatientId,
                                Message = message,
                                Title = title,
                                NotifType = "AppointmentCreated",
                            });

                        await SendAppointmentConfirmationEmailAsync(returnData.ResponseId);
                    }
                    catch (Exception ex)
                    {
                        await _errorLog.InsertErrorLog(new ErrorLogModel
                        {
                            IsDBError = false,
                            Error_Message = ex.Message,
                            Error_Procedure = "USP_SaveNotification",
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
                    Error_Procedure = appointmentProc,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }

        private async Task<bool> SendAppointmentConfirmationEmailAsync(int appointmentId)
        {
            try
            {
                var result = await GetAppointmentById(appointmentId);

                var token = _tokenRepository.GenerateAppointmentConfirmationToken(appointmentId);

                var confirmationUrl = $"{_baseUrl}/api/v1/Appointment/ConfirmAppointmentStatus?token={Uri.EscapeDataString(token)}";

                var body = $"""
                <div style="font-family:Arial,sans-serif;max-width:600px;margin:auto;
                            border:1px solid #e0e0e0;border-radius:8px;overflow:hidden;">

                    <!-- Header -->
                    <div style="background:#0066cc;padding:24px;text-align:center;">
                    <h1 style="color:white;margin:0;">Appointment Booked</h1>
                    <p style="color:#cce5ff;margin:8px 0 0;">Please confirm your attendance</p>
                    </div>

                    <!-- Body -->
                    <div style="padding:24px;">
                    <p>Hi <strong>{result.PatientName}</strong>,</p>
                    <p>Your appointment has been successfully booked. 
                        Kindly confirm your attendance by clicking the button below.</p>

                    <!-- Details Table -->
                    <table style="width:100%;border-collapse:collapse;margin:16px 0;">
                        <tr style="background:#f5f5f5;">
                        <td style="padding:10px;font-weight:bold;width:40%;">Doctor</td>
                        <td style="padding:10px;">{result.Name}</td>
                        </tr>
                        <tr>
                        <td style="padding:10px;font-weight:bold;">Hospital</td>
                        <td style="padding:10px;">{result.HospitalName}</td>
                        </tr>
                        <tr style="background:#f5f5f5;">
                        <td style="padding:10px;font-weight:bold;">Date</td>
                        <td style="padding:10px;">{result.SlotDate}</td>
                        </tr>
                        <tr>
                        <td style="padding:10px;font-weight:bold;">Time</td>
                        <td style="padding:10px;">{result.SlotStartTime}</td>
                        </tr>
                        <tr style="background:#f5f5f5;">
                        <td style="padding:10px;font-weight:bold;">Visit Type</td>
                        <td style="padding:10px;">{result.VisitType}</td>
                        </tr>
                    </table>

                    <!-- Confirm Button -->
                    <div style="text-align:center;margin:32px 0 16px;">
                        <a href="{confirmationUrl}"
                            style="display:inline-block;
                                background-color:#0066cc;
                                color:#ffffff;
                                text-decoration:none;
                                font-size:15px;
                                font-weight:600;
                                padding:14px 40px;
                                border-radius:6px;
                                letter-spacing:0.3px;">
                        ✓ &nbsp; Confirm My Appointment
                        </a>
                    </div>

                    <p style="text-align:center;color:#e65c00;font-weight:bold;font-size:13px;">
                        ⚠️ This confirmation link expires in <strong>30 Minutes</strong>.
                    </p>

                    <p style="color:#666;font-size:13px;">
                        If you did not book this appointment, please ignore this email.
                    </p>
                    </div>

                    <!-- Footer -->
                    <div style="background:#f5f5f5;padding:16px;text-align:center;">
                    <p style="color:#999;font-size:12px;margin:0;">
                        MediBook — Your Health, Our Priority
                    </p>
                    </div>

                </div>
                """;
                return await _emailService.SendEmailAsync(new EmailModel
                {
                    ToEmail = result.PatientEmail,
                    ToName = result.PatientName,
                    Subject = $"Action Required: Confirm your appointment with {result.Name}",
                    Body = body,
                    IsHtml = true
                });
            }
            catch(Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel()
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = "SendAppointmentConfirmationEmailAsync",
                    Error_Trace = ex.StackTrace
                });
            }
            return true;
        }
        public async Task<ResponseModel> UpdateAppointmentScheduleAsync(UpdateAppointmentScheduleRequestModel model)
        {
            string procName = "USP_UpdateAppointmentSchedule";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("AppointmentId", model.AppointmentId);
                param.Add("PatientId", model.PatientId);
                param.Add("AssociateId", model.AssociateId);
                param.Add("SlotId", model.SlotId);
                param.Add("VisitPurpose", model.VisitPurpose);
                param.Add("VisitType", model.VisitType);
                param.Add("RescheduleReason", model.RescheduleReason);
                param.Add("LastUpdatedBy", model.LastUpdatedBy);
                param.Add("AssociateRole", model.AssociateRole);

                returnData = await _context.QuerySingleStoredProcAsync<ResponseModel>(procName, param);
            }
            catch(Exception ex)
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

        public async Task<ResponseModel> CancelAppointmentByIdAsync(CancelAppointmentScheduleRequestModel model)
        {
            string procName = "USP_CancelAppointment";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("AppointmentId", model.AppointmentId);
                param.Add("PatientId", model.PatientId);
                param.Add("CancelReason", model.CancelReason);
                param.Add("LastUpdatedBy", model.LastUpdatedBy);
                param.Add("AssociateRole", model.AssociateRole);

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

        public async Task<List<PatientProfileModel>> GetMyAppointmentListByAssociateIdAsync(int associateId)
        {
            string procName = "USP_GetMyAppointmentListByAssociateId";
            List<PatientProfileModel> returnData = new List<PatientProfileModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("AssociateId", associateId);

                returnData = await _context.QueryStoredProcListAsync<PatientProfileModel>(procName, param);
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

        public async Task<ResponseModel> ConfirmAppointmentStatusAsync(int appointmentId)
        {
            string procName = "USP_ConfirmAppointmentStatus";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("AppointmentId", appointmentId);

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
    }
}
