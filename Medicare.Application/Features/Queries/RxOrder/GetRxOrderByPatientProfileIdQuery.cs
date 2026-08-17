using MediatR;
using Medicare.Application.Models.Orders;
using Medicare.Application.Models.RxOrder;

namespace Medicare.Application.Features.Queries.RxOrder 
{
    public record GetRxOrderByPatientProfileIdQuery(GetRxOrderRequestModel model) : IRequest<List<RxOrderDetailModel>>;
}
