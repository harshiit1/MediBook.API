using MediatR;
using Medicare.Application.Models.Associate;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Features.Commands.Associate
{
    public record CreateAssociateScheduleCommand(AssociateScheduleModel model) : IRequest<ResponseModel>;
}
