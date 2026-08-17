using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Patient;

namespace Medicare.Application.Interfaces.IPatient
{
    public interface IPatientRepository
    {
        Task<ResponseModel> CreatePatientDetails(PatientMasterModel model);
        Task<ResponseModel> UpdatePatientDetails(UpdatePatientRequestModel model);
        Task<PatientMasterModel> GetPatientByContact(string contactNo);
        Task<PatientDetailModel> GetPatientById(int Id);
        Task<PatientDetailModel> GetPatientInfoByUsername(string Username);
        Task<List<PatientProfileModel>> GetPatientProfileListByIdAsync(int patientId);
        Task<PatientProfileModelDto> GetPatientProfileByProfileIdAsync(int profileId);
    }
}
