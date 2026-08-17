using MediatR;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Lab;

namespace Medicare.Application.Features.Commands.LabResult
{
    public record CreateLabResultCommand(LabResultModel model) : IRequest<ResponseModel>;
}
