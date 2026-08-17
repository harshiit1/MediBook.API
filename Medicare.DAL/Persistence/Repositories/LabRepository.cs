using Dapper;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Interfaces.ILab;
using Medicare.Application.Models.CommonModels.ErrorLog;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Lab;
using Medicare.DAL.Persistence.Dapper;

namespace Medicare.DAL.Persistence.Repositories
{
    public class LabRepository : ILabRepository
    {
        private readonly IDapperContext _context;
        private readonly IErrorLogRepository _errorLog;
        public LabRepository(IDapperContext context, IErrorLogRepository errorLogRepository)
        {
            _context = context;
            _errorLog = errorLogRepository;
        }

        public async Task<ResponseModel> CreateLabResultAsync(LabResultModel model)
        {
            string procName = "USP_CreateLabResultReport";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("Order_Test_ID", model.OrderTestId);
                param.Add("ComponentResults", System.Text.Json.JsonSerializer.Serialize(model.ComponentResults));

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

        public async Task<LabResultSummaryModel> GetLabResultDetailByIdAsync(int id)
        {
            string procName = "USP_GetLabResultDetailById";
            LabResultSummaryModel returnData = new LabResultSummaryModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("Id", id);

                returnData = await _context.QuerySingleStoredProcAsync<LabResultSummaryModel>(procName, param);
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

        public async Task<List<LabResultSummaryModel>> GetLabResultDetailByPatientIdAsync(int patientId)
        {
            string procName = "USP_GetLabResultsByPatientId";
            List<LabResultSummaryModel> returnData = new List<LabResultSummaryModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("PatientId", patientId);

                returnData = await _context.QueryStoredProcListAsync<LabResultSummaryModel>(procName, param);
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

        public async Task<List<LabResultSummaryModel>> GetLabResultDetailByProfileIdAsync(int profileId)
        {
            string procName = "USP_GetLabResultsByProfileId";
            List<LabResultSummaryModel> returnData = new List<LabResultSummaryModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("ProfileId", profileId);

                returnData = await _context.QueryStoredProcListAsync<LabResultSummaryModel>(procName, param);
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
