using Dapper;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Models.CommonModels.ErrorLog;
using Medicare.DAL.Persistence.Dapper;

namespace Medicare.DAL.Persistence.Repositories
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly IDapperContext _context;
        public ErrorLogRepository(IDapperContext context)
        {
            _context = context;
        }
        public async Task<List<ErrorLogModel>> GetErrorLogQuery()
        {
            List<ErrorLogModel> returnData = new List<ErrorLogModel>();
            string procName = "USP_GetErrorLogs";
            returnData = await _context.QueryStoredProcListAsync<ErrorLogModel>(procName);

            return returnData;
        }

        public async Task<ErrorLogModel> InsertErrorLog(ErrorLogModel model)
        {
            ErrorLogModel returnData = new ErrorLogModel();
            string procName = "USP_InsertErrorLogs"; 
            var param = new DynamicParameters();
            param.Add("IsDBError", model.IsDBError);
            param.Add("Error_Message", model.Error_Message);
            param.Add("Error_Procedure", model.Error_Procedure);
            param.Add("Error_Trace", model.Error_Trace);

            returnData = await _context.QuerySingleStoredProcAsync<ErrorLogModel>(procName, param);
            
            return returnData;
        }
    }
}
