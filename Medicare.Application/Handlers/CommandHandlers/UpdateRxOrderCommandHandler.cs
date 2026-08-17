using MediatR;
using Medicare.Application.Features.Commands.RxOrder;
using Medicare.Application.Interfaces.IOrders;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class UpdateRxOrderHandler : IRequestHandler<UpdateRxOrderCommand, ResponseModel>
    {
        private readonly IRxOrderRepository _rxRepository;

        public UpdateRxOrderHandler(IRxOrderRepository rxRepository)
        {
            _rxRepository = rxRepository;
        }

        public async Task<ResponseModel> Handle(
            UpdateRxOrderCommand request,
            CancellationToken cancellationToken)
        {
            return await _rxRepository.UpdateRxOrderAsync(request.Model);
        }
    }
}
