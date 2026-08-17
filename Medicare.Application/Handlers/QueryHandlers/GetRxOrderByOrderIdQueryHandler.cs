using MediatR;
using Medicare.Application.Features.Queries.RxOrder;
using Medicare.Application.Interfaces.IOrders;
using Medicare.Application.Models.Orders;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetRxOrderByOrderIdQueryHandler : IRequestHandler<GetRxOrderByOrderIdQuery, RxOrderDetailModel>
    {
        private readonly IRxOrderRepository _rxRepository;

        public GetRxOrderByOrderIdQueryHandler(IRxOrderRepository rxRepository)
        {
            _rxRepository = rxRepository;
        }

        public async Task<RxOrderDetailModel> Handle(GetRxOrderByOrderIdQuery request,  CancellationToken cancellationToken)
        {
            return await _rxRepository.GetRxOrderByOrderIdAsync(request.orderId);
        }
    }
}
