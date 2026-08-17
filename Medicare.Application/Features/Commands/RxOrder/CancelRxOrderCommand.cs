using MediatR;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Orders;

namespace Medicare.Application.Features.Commands.RxOrder
{
    public record CancelRxOrderCommand(CancelRxOrderRequestModel Model) : IRequest<ResponseModel>;
}
