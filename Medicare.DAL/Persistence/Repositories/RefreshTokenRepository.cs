using Dapper;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Interfaces.JwtToken;
using Medicare.Application.Models.CommonModels.ErrorLog;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.JwtTokens;
using Medicare.DAL.Persistence.Dapper;

namespace Medicare.DAL.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IDapperContext _context;
        private readonly IErrorLogRepository _errorLog;

        public RefreshTokenRepository(IDapperContext context, IErrorLogRepository errorLog)
        {
            _context = context;
            _errorLog = errorLog;
        }
        public async Task<ResponseModel> SaveRefreshTokenAsync(JwtRefreshTokenModel model)
        {
            string procName = "USP_SaveRefreshToken";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("UserId", model.UserId);
                param.Add("UserType", model.UserType);
                param.Add("Token", model.RefreshToken);
                param.Add("ExpiresDate", model.ExpiryDate);

                returnData = await _context.QuerySingleStoredProcAsync<ResponseModel>(procName, param);
            }
            catch (Exception ex) 
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }

        public async Task<RefreshTokenDto> ValidateRefreshTokenAsync(string token)
        {
            string procName = "USP_ValidateRefreshToken";
            RefreshTokenDto returnData = new RefreshTokenDto();
            try
            {
                var param = new DynamicParameters();
                param.Add("Token", token);

                returnData = await _context.QuerySingleStoredProcAsync<RefreshTokenDto>(procName, param);
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel
                {
                    IsDBError = false,
                    Error_Message = ex.Message,
                    Error_Procedure = procName,
                    Error_Trace = ex.StackTrace
                });
            }
            return returnData;
        }

        public async Task<ResponseModel> RevokeRefreshTokenAsync(string refreshToken, string replacedByRefreshToken = null)
        {
            string procName = "USP_RevokeRefreshToken";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("Token", refreshToken);
                param.Add("ReplacedBy", replacedByRefreshToken);

                returnData = await _context.QuerySingleStoredProcAsync<ResponseModel>(procName, param);
            }
            catch (Exception ex)
            {
                await _errorLog.InsertErrorLog(new ErrorLogModel
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
