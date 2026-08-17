using MediatR;
using Medicare.Application.Features.Commands.Authentication;
using Medicare.Application.Interfaces.IAuthRepository;
using Medicare.Application.Models.Authentication;
using Medicare.Application.Models.CommonModels.ResponseModel;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class ResetForgotPasswordCommandHandler : IRequestHandler<ResetForgotPasswordCommand, ResponseModel>
    {
        private readonly IAuthRepository _authRepository;
        private readonly PasswordHelper _passwordHelper;

        public ResetForgotPasswordCommandHandler(IAuthRepository authRepository, PasswordHelper passwordHelper)
        {
            _authRepository = authRepository;
            _passwordHelper = passwordHelper;
        }
        public async Task<ResponseModel> Handle(ResetForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.model.Password))
                return new ResponseModel
                {
                    IsSuccess = 0,
                    ResponseMessage = "Password cannot be empty."
                };

            string passwordHash = _passwordHelper.HashPassword(request.model.Password);

            var model = new ResetForgotPasswordModel
            {
                Token = request.model.Token,
                Password = passwordHash
            };

            return await _authRepository.ResetForgotPasswordAsync(model);
        }
    }
}
