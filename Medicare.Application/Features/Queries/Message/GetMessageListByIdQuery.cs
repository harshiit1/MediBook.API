using MediatR;
using Medicare.Application.Models.Message;

namespace Medicare.Application.Features.Queries.Notification
{
    public record GetMessageListByIdQuery(int id) : IRequest<List<MessageResponseModel>>;
}
