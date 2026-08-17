using Dapper;
using Medicare.Application.Interfaces.IAssociate;
using Medicare.Application.Interfaces.IDoctor;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Models.Associate;
using Medicare.Application.Models.CommonModels.ErrorLog;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.DAL.Persistence.Dapper;

namespace Medicare.DAL.Persistence.Repositories
{
    public class AssociateRepository : IAssociateRepository
    {
        private readonly IDapperContext _context;
        private readonly IErrorLogRepository _errorLog;
        private readonly IDoctorRepository _doctorRepository;
        public AssociateRepository(IDapperContext context, IErrorLogRepository errorLogRepository, IDoctorRepository doctorRepository)
        {
            _context = context;
            _errorLog = errorLogRepository;
            _doctorRepository = doctorRepository;
        }
        public async Task<List<AssociateDetailDto>> GetAssociateDetailByIdAsync(int associateId)
        {
            string procName = "USP_GetAssociateDetailById";
            List<AssociateDetailDto> returnData = new List<AssociateDetailDto>();
            try
            {
                var param = new DynamicParameters();
                param.Add("AssociateId", associateId);

                returnData = await _context.QueryStoredProcListAsync<AssociateDetailDto>(procName, param);
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
        public async Task<ResponseModel> CreateAssociateScheduleAsync(AssociateScheduleModel model)
        {
            string procName = "USP_SaveAssociateSchedule";
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
                param.Add("WorkingDays", model.WorkingDays);
                param.Add("ConsultationTime", model.ConsultationTime);
                param.Add("AverageCharge", model.AverageCharge);
                param.Add("OtpMethod", model.OtpMethod);
                param.Add("CreatedBy", model.CreatedBy);

                returnData = await _context.QuerySingleStoredProcAsync<ResponseModel>(procName, param);
                
                if (returnData.IsSuccess == 1 && returnData.ResponseId > 0)
                {
                    var slotResult = await _doctorRepository.CreateDoctorTimeSlotsAsync(model);

                    if (slotResult.IsSuccess != 1)
                    {
                        returnData.IsSuccess = 0;
                        returnData.ResponseMessage = "Slot generation failed: " + slotResult.ResponseMessage;
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

        public async Task<List<AssociateListModel>> GetAssociateListAsync()
        {
            string procName = "USP_GetAssociateList";
            List<AssociateListModel> returnData = new List<AssociateListModel>();
            try
            {
                returnData = await _context.QueryStoredProcListAsync<AssociateListModel>(procName);
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
        public async Task<AssociateDetailDto> GetAssociateInfoByUsername(string Username)
        {
            string procName = "USP_GetAssociateAccountByUsername";
            AssociateDetailDto returnData = new AssociateDetailDto();
            try
            {
                var param = new DynamicParameters();
                param.Add("Username", Username);

                returnData = await _context.QuerySingleStoredProcAsync<AssociateDetailDto>(procName, param);
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

        public async Task<ResponseModel> UpdateAssociateDetailAsync(UpdateAssociateRequestModel model)
        {
            string procName = "USP_UpdateAssociateDetail";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("AssociateId", model.AssociateId);
                param.Add("RoleId", model.RoleId);
                param.Add("DepartmentId", model.DepartmentId);
                param.Add("SpecialityId", model.SpecialityId);
                param.Add("DesignationId", model.DesignationId);
                param.Add("FromDate", model.FromDate);
                param.Add("ToDate", model.ToDate);
                param.Add("FromTime", model.FromTime);
                param.Add("ToTime", model.ToTime);
                param.Add("BreakTimeFrom", model.BreakTimeFrom);
                param.Add("BreakTimeTo", model.BreakTimeTo);
                param.Add("WorkingDays", model.WorkingDays);
                param.Add("AverageCharge", model.AverageCharge);
                param.Add("ConsultationTime", model.ConsultationTime);
                param.Add("UpdatedBy", model.UpdatedBy);

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

        public async Task<ResponseModel> DeleteAssociateAsync(DeleteAssociateRequestModel model)
        {
            string procName = "USP_DeleteAssociate";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("AssociateId", model.AssociateId);
                param.Add("IsActive", model.IsActive);
                param.Add("UpdatedBy", model.UpdatedBy);

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
