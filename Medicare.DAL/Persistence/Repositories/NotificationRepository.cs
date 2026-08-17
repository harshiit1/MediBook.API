using Dapper;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Interfaces.INotificationRepository;
using Medicare.Application.Models.CommonModels.ErrorLog;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Notification;
using Medicare.DAL.Persistence.Dapper;

namespace Medicare.DAL.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IDapperContext _context;
        private readonly IErrorLogRepository _errorLog;
        public NotificationRepository(IDapperContext context, IErrorLogRepository errorLog) 
        {
            _context = context;
            _errorLog = errorLog;
        }
        public async Task<ResponseModel> CreateAsync(SaveNotificationModel model)
        {
            ResponseModel returnData = new ResponseModel();
            string procName = "USP_SaveNotification";
            try
            {   
                var param = new DynamicParameters();
                param.Add("RefId", model.RefId);
                param.Add("UserType", model.UserType);
                param.Add("Title", model.Title);
                param.Add("NotifType", model.NotifType);
                param.Add("Message", model.Message);
                param.Add("ReferenceId", model.ReferenceId);

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
