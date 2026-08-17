using MediatR;
using Medicare.Application.Models.Associate;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Features.Commands.Associate
{
    public record DeleteAssociateCommand(DeleteAssociateRequestModel model) : IRequest<ResponseModel>;
}
