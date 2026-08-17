using Dapper;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Interfaces.IOrders;
using Medicare.Application.Models.CommonModels.ErrorLog;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Orders;
using Medicare.Application.Models.RxOrder;
using Medicare.DAL.Persistence.Dapper;

namespace Medicare.DAL.Persistence.Repositories
{
    public class RxOrderRepository : IRxOrderRepository
    {
        private readonly IDapperContext _context;
        private readonly IErrorLogRepository _errorLog;

        public RxOrderRepository(IDapperContext context, IErrorLogRepository errorLog)
        {
            _context = context;
            _errorLog = errorLog;
        }

        public async Task<List<RxOrderDetailModel>> GetRxOrderByPatientIdAsync(GetRxOrderRequestModel model)
        {
            string procName = "USP_GetRxOrdersByPatientId";
            List<RxOrderDetailModel> returnData = new List<RxOrderDetailModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("PatientId", model.PatientId);
                param.Add("ProfileId", model.ProfileId);

                returnData = await _context.QueryStoredProcListAsync<RxOrderDetailModel>(procName, param);
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

        public async Task<RxOrderDetailModel> GetRxOrderByOrderIdAsync(int orderId)
        {
            string procName = "USP_GetRxOrderByOrderId";
            RxOrderDetailModel returnData = new RxOrderDetailModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("OrderId", orderId);

                returnData = await _context.QuerySingleStoredProcAsync<RxOrderDetailModel>(procName, param);
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

        public async Task<ResponseModel> CreateRxOrderAsync(CreateRxOrderRequestModel model)
        {
            string procName = "USP_CreateRxOrder";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("PatientId", model.PatientId);
                param.Add("ProfileId", model.ProfileId);
                param.Add("AssociateId", model.AssociateId);
                param.Add("PharmacyId", model.PharmacyId);
                param.Add("DrugName", model.DrugName);
                param.Add("Dosage", model.Dosage);
                param.Add("Frequency", model.Frequency);
                param.Add("DurationDays", model.DurationDays);
                param.Add("Instructions", model.Instructions);
                param.Add("ExpiryDate", model.ExpiryDate);

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

        public async Task<ResponseModel> CancelRxOrderAsync(CancelRxOrderRequestModel model)
        {
            string procName = "USP_CancelRxOrder";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("OrderId", model.OrderId);
                param.Add("PatientId", model.PatientId);
                param.Add("CancelReason", model.CancelReason);
               
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

        public async Task<ResponseModel> UpdateRxOrderAsync(UpdateRxOrderRequestModel model)
        {
            string procName = "USP_UpdateRxOrder";
            ResponseModel returnData = new ResponseModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("OrderId", model.OrderId);
                param.Add("PatientId", model.PatientId);
                param.Add("PharmacyId", model.PharmacyId);
                param.Add("DrugName", model.DrugName);
                param.Add("Dosage", model.Dosage);
                param.Add("Frequency", model.Frequency);
                param.Add("DurationDays", model.DurationDays);
                param.Add("Instructions", model.Instructions);
                param.Add("ExpiryDate", model.ExpiryDate);

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
