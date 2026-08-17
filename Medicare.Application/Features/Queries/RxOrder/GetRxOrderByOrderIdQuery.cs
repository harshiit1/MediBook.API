using MediatR;
using Medicare.Application.Models.Orders;

namespace Medicare.Application.Features.Queries.RxOrder
{
    public record GetRxOrderByOrderIdQuery(int orderId)  : IRequest<RxOrderDetailModel>;
}
