using MediatR;
using Medicare.Application.Features.Commands.Authentication;
using Medicare.Application.Interfaces.IAuthRepository;
using Medicare.Application.Interfaces.UserRepository;
using Medicare.Application.Models.Authentication;
using Medicare.Application.Models.CommonModels.ResponseModel;
using System.Security.Cryptography;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ResponseModel>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;
        private readonly PasswordHelper _passwordHelper;
        public ForgotPasswordCommandHandler(IAuthRepository authRepository, PasswordHelper passwordHelper, IUserRepository userRepository)
        {
            _authRepository = authRepository;
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
        }
        public async Task<ResponseModel> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseModel
            {
                Status = 1,
                IsSuccess = 1,
                ResponseMessage = $"An OTP has been sent to {request.model.Email}."
            };

            var user = await _userRepository.GetUserByEmailAsync(request.model.Email);
            if (user is null || user.IsSuccess == 0)
                return new ResponseModel
                {
                    Status = 0,
                    IsSuccess = 0,
                    ResponseMessage = user.ResponseMessage ?? "An Error Occured while Fetching the User Details."
                };

            // Generate 6-digit OTP and hash it
            string otpCode = GenerateOtp();
            string otpHash = _passwordHelper.HashPassword(otpCode);

            var otpModel = new OtpDetailModel
            {
                UserId = user.UserId,
                UserType = user.UserType,
                OtpHash = otpHash,
                OtpExpiry = DateTime.UtcNow.AddMinutes(2),
                OtpAttempts = 0
            };

            var result = await _authRepository.SaveOtpAsync(otpModel);
            
            if (result.IsSuccess == 0)
                return new ResponseModel
                {
                    Status = 0,
                    IsSuccess = 0,
                    ResponseMessage = result.ResponseMessage ?? "An Error Occured while Saving the OTP Details."
                }; 

            await _authRepository.SendOtpEmailAsync(user.Email, user.FullName, otpCode);

            return response;
        }
        private static string GenerateOtp()
        {
            // Cryptographically secure random 4-digit OTP
            var bytes = RandomNumberGenerator.GetBytes(4);
            var number = BitConverter.ToUInt32(bytes, 0) % 10_000;
            return number.ToString("D4");
        }
    }
}
