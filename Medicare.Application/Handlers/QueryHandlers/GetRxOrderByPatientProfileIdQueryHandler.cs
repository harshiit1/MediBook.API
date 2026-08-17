using MediatR;
using Medicare.Application.Features.Queries.RxOrder;
using Medicare.Application.Interfaces.IOrders;
using Medicare.Application.Models.Orders;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetRxOrderByPatientProfileIdQueryHandler : IRequestHandler<GetRxOrderByPatientProfileIdQuery, List<RxOrderDetailModel>>
    {
        private readonly IRxOrderRepository _rxRepository;

        public GetRxOrderByPatientProfileIdQueryHandler(IRxOrderRepository rxRepository)
        {
            _rxRepository = rxRepository;
        }

        public async Task<List<RxOrderDetailModel>> Handle(GetRxOrderByPatientProfileIdQuery request,  CancellationToken cancellationToken)
        {
            return await _rxRepository.GetRxOrderByPatientIdAsync(request.model);
        }
    }
}
