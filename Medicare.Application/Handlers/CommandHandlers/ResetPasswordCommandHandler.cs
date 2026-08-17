using MediatR;
using Medicare.Application.Features.Commands.Authentication;
using Medicare.Application.Interfaces.IAuthRepository;
using Medicare.Application.Interfaces.JwtToken;
using Medicare.Application.Models.CommonModels.ResponseModel;
using System.Security.Claims;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResponseModel>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtTokenRepository _jwtTokenRepository;
        private readonly PasswordHelper _passwordHelper;
        public ResetPasswordCommandHandler(
            IAuthRepository authRepository,
            IJwtTokenRepository jwtTokenRepository,
            PasswordHelper passwordHelper)
        {
            _authRepository = authRepository;
            _jwtTokenRepository = jwtTokenRepository;
            _passwordHelper = passwordHelper;
        }

        public async Task<ResponseModel> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            ClaimsPrincipal principal;
            try
            {
                principal = _jwtTokenRepository.ValidatePasswordResetToken(request.model.Token);
            }
            catch (Exception)
            {
                return new ResponseModel
                {
                    IsSuccess = 0,
                    Status = 0,
                    ResponseMessage = "Invalid or expired reset link. Please request a new one."
                };
            }

            var userId = principal.FindFirst("userId").Value;

            if (string.IsNullOrEmpty(userId))
                return new ResponseModel
                {
                    IsSuccess = 0,
                    Status = 0,
                    ResponseMessage = "Invalid token claims."
                };

            var passwordHash = _passwordHelper.HashPassword(request.model.Password);

            return await _authRepository.ResetPasswordAsync(Guid.Parse(userId), passwordHash);
        }
    }
}
