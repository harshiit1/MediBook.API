using Dapper;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Interfaces.UserRepository;
using Medicare.Application.Models.CommonModels.ErrorLog;
using Medicare.Application.Models.User;
using Medicare.DAL.Persistence.Dapper;

namespace Medicare.DAL.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDapperContext _context;
        private readonly IErrorLogRepository _errorLog;
        public UserRepository(IDapperContext context, IErrorLogRepository errorLog)
        {
            _context = context;
            _errorLog = errorLog;
        }

        public async Task<UserInfoDataModel> GetUserInfoAsync(string Username)
        {
            string procName = "USP_GetUserInfo";
            UserInfoDataModel returnData = new UserInfoDataModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("Username", Username);

                returnData = await _context.QuerySingleStoredProcAsync<UserInfoDataModel>(procName, param);
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

        public async Task<UserInfoDataModel> GetUserByIdAsync(int Id)
        {
            string procName = "USP_GetUserById";
            UserInfoDataModel returnData = new UserInfoDataModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("UserId", Id);

                returnData = await _context.QuerySingleStoredProcAsync<UserInfoDataModel>(procName, param);

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
        public async Task<UserDetailModel> GetUserByEmailAsync(string email)
        {
            string procName = "USP_GetUserByEmail";
            UserDetailModel returnData = new UserDetailModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("Email", email);

                returnData = await _context.QuerySingleStoredProcAsync<UserDetailModel>(procName, param);

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
