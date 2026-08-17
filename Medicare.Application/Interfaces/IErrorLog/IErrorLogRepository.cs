using Medicare.Application.Models.CommonModels.ErrorLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medicare.Application.Interfaces.IErrorLog
{
    public interface IErrorLogRepository
    {
        Task<List<ErrorLogModel>> GetErrorLogQuery();
        Task<ErrorLogModel> InsertErrorLog(ErrorLogModel Model);
    }
}
