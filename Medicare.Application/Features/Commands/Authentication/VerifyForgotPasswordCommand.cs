using MediatR;
using Medicare.Application.Models.Authentication;

namespace Medicare.Application.Features.Commands.Authentication
{
    public record VerifyForgotPasswordCommand(VerifyForgotPasswordModel model) : IRequest<VerifyForgotPasswordResponseModel>; 
}
