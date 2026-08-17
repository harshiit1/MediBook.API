using Medicare.Application.Models.Doctor;
using Medicare.Application.Models.Associate;
using Medicare.Application.Models.Speciality;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Interfaces.IDoctor
{
    public interface IDoctorRepository
    {
        Task<List<DoctorItemModel>> GetDoctorListAsync();
        Task<List<DoctorSpecialityDataModel>> GetDoctorSpecialityListAsync(string? doctorName, string? departmentName);
        Task<ResponseModel> CreateDoctorTimeSlotsAsync(AssociateScheduleModel model);
        Task<List<DoctorAvailabilityModel>> GetDoctorTimeSlotsAsync(DoctorTimeSlotRequestModel model);
    }
}