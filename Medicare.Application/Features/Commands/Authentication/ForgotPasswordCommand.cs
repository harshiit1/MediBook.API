using MediatR;
using Medicare.Application.Models.Authentication;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Features.Commands.Authentication
{
    public record ForgotPasswordCommand(RequestOtpModel model) : IRequest<ResponseModel>;
}
