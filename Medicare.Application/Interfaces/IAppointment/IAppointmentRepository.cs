using Medicare.Application.Models.Appointment;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Patient;

namespace Medicare.Application.Interfaces.IAppointment
{
    public interface IAppointmentRepository
    {
        Task<List<PatientAppointmentModel>> GetMyAppointmentListByPatientIdAsync(int patientId);
        Task<List<PatientProfileModel>> GetMyAppointmentListByAssociateIdAsync(int associateId);
        Task<List<AvailableAppointmentModel>> GetAvailableAppointmentsAsync(int associateId);
        Task<AppointmentDetailModelDto> GetAppointmentById(int appointmentId);
        Task<ResponseModel> CreateAppointmentAsync(AppointmentMasterModel model);
        Task<ResponseModel> UpdateAppointmentScheduleAsync(UpdateAppointmentScheduleRequestModel model);
        Task<ResponseModel> CancelAppointmentByIdAsync(CancelAppointmentScheduleRequestModel model);
        Task<ResponseModel> ConfirmAppointmentStatusAsync(int appointmentId);
    }
}
