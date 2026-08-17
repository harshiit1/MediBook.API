using Dapper;
using Medicare.Application.Interfaces.IDoctor;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Models.Associate;
using Medicare.Application.Models.CommonModels.ErrorLog;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Doctor;
using Medicare.Application.Models.Speciality;
using Medicare.DAL.Persistence.Dapper;

namespace Medicare.DAL.Persistence.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IDapperContext _context;
        private readonly IErrorLogRepository _errorLog;
        public DoctorRepository(IDapperContext context, IErrorLogRepository errorLogRepository) 
        {
            _context = context;
            _errorLog = errorLogRepository;
        }
        public async Task<List<DoctorItemModel>> GetDoctorListAsync()
        {
            string procName = "USP_GetDoctorList";
            List<DoctorItemModel> returnData = new List<DoctorItemModel>();
            try
            {
                returnData = await _context.QueryStoredProcListAsync<DoctorItemModel>(procName);
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

        public async Task<List<DoctorSpecialityDataModel>> GetDoctorSpecialityListAsync(string? doctorName, string? departmentName)
        {
            string procName = "USP_GetDoctorSpecialityList";
            List<DoctorSpecialityDataModel> returnData = new List<DoctorSpecialityDataModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("DoctorName", doctorName);
                param.Add("DepartmentName", departmentName);

                returnData = await _context.QueryStoredProcListAsync<DoctorSpecialityDataModel>(procName, param);
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

        public async Task<ResponseModel> CreateDoctorTimeSlotsAsync(AssociateScheduleModel model)
        {
            string procName = "USP_CreateDoctorTimeSlots";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("AssociateId", model.AssociateId);
                param.Add("FromDate", model.FromDate);
                param.Add("ToDate", model.ToDate);
                param.Add("FromTime", model.FromTime);
                param.Add("ToTime", model.ToTime);
                param.Add("BreakTimeFrom", model.BreakTimeFrom);
                param.Add("BreakTimeTo", model.BreakTimeTo);
                param.Add("ConsultationTime", model.ConsultationTime);
                param.Add("CreatedBy", model.CreatedBy);

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
        public async Task<List<DoctorAvailabilityModel>> GetDoctorTimeSlotsAsync(DoctorTimeSlotRequestModel model)
        {
            string procName = "USP_GetDoctorTimeSlots";
            List<DoctorAvailabilityModel> returnData = new List<DoctorAvailabilityModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("AssociateId", model.AssociateId);
                param.Add("FromDate", model.FromDate);
                param.Add("ToDate", model.ToDate);
                returnData = await _context.QueryStoredProcListAsync<DoctorAvailabilityModel>(procName, param);
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
