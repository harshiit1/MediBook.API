using MediatR;
using Medicare.Application.Features.Commands.RxOrder;
using Medicare.Application.Interfaces.IOrders;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class CancelRxOrderHandler : IRequestHandler<CancelRxOrderCommand, ResponseModel>
    {
        private readonly IRxOrderRepository _rxRepository;

        public CancelRxOrderHandler(IRxOrderRepository rxRepository)
        {
            _rxRepository = rxRepository;
        }

        public async Task<ResponseModel> Handle(
            CancelRxOrderCommand request,
            CancellationToken cancellationToken)
        {
            return await _rxRepository.CancelRxOrderAsync(request.Model);
        }
    }
}
