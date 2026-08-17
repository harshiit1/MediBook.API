using Dapper;
using Medicare.DAL.Persistence.Dapper;
using Medicare.Application.Models.Billing;
using Medicare.Application.Interfaces.IBilling;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Models.CommonModels.ErrorLog;

namespace Medicare.DAL.Persistence.Repositories
{
    public class BillingRepository : IBillingRepository
    {
        private readonly IDapperContext _context;
        private readonly IErrorLogRepository _errorLog;
        public BillingRepository(IDapperContext context, IErrorLogRepository errorLogRepository)
        {
            _context = context;
            _errorLog = errorLogRepository;
        }

        public async Task<BillingSummaryModel> GetBillsByIdAsync(int id)
        {
            string procName = "USP_GetBillsById";
            BillingSummaryModel returnData = new BillingSummaryModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("Id", id);

                returnData = await _context.QuerySingleStoredProcAsync<BillingSummaryModel>(procName, param);
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
        public async Task<List<BillingSummaryModel>> GetBillsByPatientIdAsync(int patientId)
        {
            string procName = "USP_GetBillsByPatientId";
            List<BillingSummaryModel> returnData = new List<BillingSummaryModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("PatientId", patientId);

                returnData = await _context.QueryStoredProcListAsync<BillingSummaryModel>(procName, param);
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
