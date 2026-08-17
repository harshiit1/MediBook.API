using MediatR;
using Medicare.Application.Features.Commands.Message;
using Medicare.Application.Interfaces.IMessage;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class UpdateMessageToReadCommandHandler : IRequestHandler<UpdateMessageToReadCommand, ResponseModel>
    {
        private readonly IMessageRepository _messageRepository;
        public UpdateMessageToReadCommandHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<ResponseModel> Handle(UpdateMessageToReadCommand request, CancellationToken cancellationToken)
        {
            return await _messageRepository.UpdateMessageToReadAsync(request.model);
        }
    }
}
