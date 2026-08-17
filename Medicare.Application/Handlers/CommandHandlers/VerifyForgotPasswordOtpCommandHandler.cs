using MediatR;
using Medicare.Application.Features.Commands.Authentication;
using Medicare.Application.Interfaces.IAuthRepository;
using Medicare.Application.Interfaces.UserRepository;
using Medicare.Application.Models.Authentication;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class VerifyForgotPasswordOtpCommandHandler : IRequestHandler<VerifyForgotPasswordCommand, VerifyForgotPasswordResponseModel>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;
        private readonly PasswordHelper _passwordHelper;
        public VerifyForgotPasswordOtpCommandHandler(IAuthRepository authRepository, IUserRepository userRepository, PasswordHelper passwordHelper)
        {
            _authRepository = authRepository;
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
        }
        public async Task<VerifyForgotPasswordResponseModel> Handle(VerifyForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.model.Email);
            if (user is null || user.IsSuccess == 0)
                return new VerifyForgotPasswordResponseModel
                {
                    IsSuccess = 0,
                    Status = 0,
                    ResponseMessage = "Invalid request."
                };

            var otpDetail = await _authRepository.GetOtpDetailByUserIdAsync(user.UserId);
            if (otpDetail.IsSuccess == 0)
                return new VerifyForgotPasswordResponseModel
                {
                    IsSuccess = 0,
                    Status = 0,
                    ResponseMessage = "OTP not found."
                };

            bool valid = _passwordHelper.VerifyPassword(request.model.OtpCode, otpDetail.OtpHash);
            if (!valid)
            {
                await _authRepository.IncrementOtpAttemptsAsync(user.UserId);
                return new VerifyForgotPasswordResponseModel
                {
                    IsSuccess = 0,
                    Status = 0,
                    ResponseMessage = "Invalid OTP."
                };
            }

            // Mark OTP as used
            await _authRepository.ClearForgotPasswordOtpAsync(user.UserId);

            // Generate reset token 
            Guid resetToken = Guid.NewGuid();

            var result = await _authRepository.SavePasswordResetTokenAsync(user.UserId, resetToken);
            if (result.IsSuccess == 0)
                return new VerifyForgotPasswordResponseModel
                {
                    IsSuccess = 0,
                    Status = 0,
                    ResponseMessage = "Failed to issue reset token."
                };

            return new VerifyForgotPasswordResponseModel
            {
                Status = 1,
                IsSuccess = 1,
                ResponseMessage = "OTP verified successfully.",
                Token = resetToken
            };
        }
    }
}
