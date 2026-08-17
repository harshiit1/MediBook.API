using MediatR;
using Medicare.Application.Models.Associate;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Features.Commands.Associate
{
    public record CreateAssociateCommand(CreateAssociateRequestModel model) : IRequest<ResponseModel>;
}
