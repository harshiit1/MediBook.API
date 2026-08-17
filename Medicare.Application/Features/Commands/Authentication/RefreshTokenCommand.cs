using MediatR;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.JwtTokens;

namespace Medicare.Application.Features.Commands.Authentication
{
    public record RefreshTokenCommand(RefreshTokenRequestModel model) : IRequest<RefreshTokenResponseModel>;
}
