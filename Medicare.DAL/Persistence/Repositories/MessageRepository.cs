using Dapper;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Interfaces.IMessage;
using Medicare.Application.Models.CommonModels.ErrorLog;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Message;
using Medicare.DAL.Persistence.Dapper;

namespace Medicare.DAL.Persistence.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IDapperContext _context;
        private readonly IErrorLogRepository _errorLog;
        public MessageRepository(IDapperContext context, IErrorLogRepository errorLog)
        {
            _context = context;
            _errorLog = errorLog;
        }
        public async Task<List<MessageResponseModel>> GetNotificationListById(int id)
        {
            List<MessageResponseModel> returnData = new List<MessageResponseModel>();
            string procName = "USP_GetMessageListById";
            try
            {
                var param = new DynamicParameters();
                param.Add("Id", id);

                returnData = await _context.QueryStoredProcListAsync<MessageResponseModel>(procName, param);
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

        public async Task<ResponseModel> UpdateMessageToReadAsync(UpdateMessageRequestModel model)
        {
            string procName = "USP_UpdateMessageToReadAsync";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("MessageId", model.MessageId);
                param.Add("IsRead", model.IsRead);

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
