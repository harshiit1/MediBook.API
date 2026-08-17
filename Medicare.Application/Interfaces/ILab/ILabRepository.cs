using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Lab;

namespace Medicare.Application.Interfaces.ILab
{
    public interface ILabRepository
    {
        Task<ResponseModel> CreateLabResultAsync(LabResultModel model);
        Task<LabResultSummaryModel> GetLabResultDetailByIdAsync(int id);
        Task<List<LabResultSummaryModel>> GetLabResultDetailByPatientIdAsync(int patientId);
        Task<List<LabResultSummaryModel>> GetLabResultDetailByProfileIdAsync(int profileId);
    }
}
