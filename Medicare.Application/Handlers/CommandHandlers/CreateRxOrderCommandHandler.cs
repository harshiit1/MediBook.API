using MediatR;
using Medicare.Application.Features.Commands.RxOrder;
using Medicare.Application.Interfaces.IOrders;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class CreateRxOrderHandler : IRequestHandler<CreateRxOrderCommand, ResponseModel>
    {
        private readonly IRxOrderRepository _rxRepository;

        public CreateRxOrderHandler(IRxOrderRepository rxRepository)
        {
            _rxRepository = rxRepository;
        }

        public async Task<ResponseModel> Handle(
            CreateRxOrderCommand request,
            CancellationToken cancellationToken)
        {
            return await _rxRepository.CreateRxOrderAsync(request.Model);
        }
    }
}
