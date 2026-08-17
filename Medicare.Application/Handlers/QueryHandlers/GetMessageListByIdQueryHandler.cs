using MediatR;
using Medicare.Application.Features.Queries.Notification;
using Medicare.Application.Interfaces.IMessage;
using Medicare.Application.Models.Message;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetMessageListByIdQueryHandler : IRequestHandler<GetMessageListByIdQuery, List<MessageResponseModel>>
    {
        private readonly IMessageRepository _messageRepository;
        public GetMessageListByIdQueryHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<List<MessageResponseModel>> Handle(GetMessageListByIdQuery request, CancellationToken cancellationToken)
        {
            return await _messageRepository.GetNotificationListById(request.id);
        }
    }
}
