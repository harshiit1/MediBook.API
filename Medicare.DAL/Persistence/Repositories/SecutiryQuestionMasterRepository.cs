using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Interfaces.ISecurityQuestionsRepository;
using Medicare.Application.Models.CommonModels.ErrorLog;
using Medicare.Application.Models.User;
using Medicare.DAL.Persistence.Dapper;

namespace Medicare.DAL.Persistence.Repositories
{
    public class SecurityQuestionMasterRepository : ISecurityQuestionsRepository
    {
        private readonly DapperContext _context;
        private readonly IErrorLogRepository _errorLog;
        public SecurityQuestionMasterRepository(DapperContext context, IErrorLogRepository errorLog)
        {
            _context = context;
            _errorLog = errorLog;
        }
        public async Task<List<SecurityQuestionDataModel>> GetSecurityQuestionMasterAsync()
        {
            string procName = "USP_GetSecurityQuestionMaster";
            List<SecurityQuestionDataModel> returnData = new List<SecurityQuestionDataModel>();
            try 
            {
                returnData = await _context.QueryStoredProcListAsync<SecurityQuestionDataModel>(procName);
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
