using MediatR;
using Medicare.Application.Features.Commands.Authentication;
using Medicare.Application.Interfaces.IAuthRepository;
using Medicare.Application.Interfaces.IEmail;
using Medicare.Application.Interfaces.UserRepository;
using Medicare.Application.Models.Authentication;
using Medicare.Application.Models.CommonModels.ResponseModel;
using System.Security.Cryptography;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class RequestOtpCommandHandler : IRequestHandler<RequestOtpCommand, ResponseModel>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IEmailJobService _emailJobService;
        private readonly IUserRepository _userRepository;
        private readonly PasswordHelper _passwordHelper;
        public RequestOtpCommandHandler(IAuthRepository authRepository, IEmailJobService emailJobService, IUserRepository userRepository, PasswordHelper passwordHelper)
        {
            _authRepository = authRepository;
            _emailJobService = emailJobService;
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
        }

        public async Task<ResponseModel> Handle(RequestOtpCommand request, CancellationToken ct)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.model.Email);
            if (user.Status == 0) 
            {
                return new ResponseModel
                {
                    IsSuccess = 0,
                    Status = 0,
                    ResponseId = 0,
                    ResponseMessage = "Invalid Email Address."
                };
            }
            // Generate OTP
            var rawOtp = GenerateOtp();

            var otpHash = _passwordHelper.HashPassword(rawOtp);

            var expiry = DateTime.UtcNow.AddMinutes(5);

            var otpModel = new OtpDetailModel
            { 
                UserId = user.UserId,
                UserType = user.UserType,
                OtpHash = otpHash,
                OtpExpiry = expiry,
                OtpAttempts = 0
            };

            //Save OTP details to database
            await _authRepository.SaveOtpAsync(otpModel);

            var jobId = _emailJobService.QueueOtpEmail(
                toEmail: request.model.Email,
                toName: request.model.Email,
                otpCode: rawOtp
            );

            return new ResponseModel()
            {
                Status = 1,
                IsSuccess = 1,
                ResponseMessage = $"OTP has been sent to {request.model.Email}.",
                ResponseId = 0
            };
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
