using MediatR;
using Medicare.Application.Models.CommonModels.ErrorLog;

namespace Medicare.Application.Features.Commands.ErrorLog
{
    public record ErrorLogCommand(ErrorLogModel Model) : IRequest<ErrorLogModel>;   
}
