using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Orders;
using Medicare.Application.Models.RxOrder;

namespace Medicare.Application.Interfaces.IOrders
{
    public interface IRxOrderRepository
    {
        Task<List<RxOrderDetailModel>> GetRxOrderByPatientIdAsync(GetRxOrderRequestModel model);
        Task<RxOrderDetailModel> GetRxOrderByOrderIdAsync(int orderId);
        Task<ResponseModel> CreateRxOrderAsync(CreateRxOrderRequestModel model);
        Task<ResponseModel> UpdateRxOrderAsync(UpdateRxOrderRequestModel model);
        Task<ResponseModel> CancelRxOrderAsync(CancelRxOrderRequestModel model);
    }
}
