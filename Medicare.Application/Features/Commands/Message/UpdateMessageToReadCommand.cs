using MediatR;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Message;

namespace Medicare.Application.Features.Commands.Message
{
    public record UpdateMessageToReadCommand(UpdateMessageRequestModel model) : IRequest<ResponseModel>;
}
