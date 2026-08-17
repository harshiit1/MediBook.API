using MediatR;
using Medicare.Application.Features.Commands.Authentication;
using Medicare.Application.Interfaces.IAuthRepository;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.User;
using System.Security.Cryptography;
using System.Text;
namespace Medicare.Application.Handlers.CommandHandlers
{
    public class CreateUserHandler : IRequestHandler<UserCommand, ResponseModel>
    {
        private readonly IAuthRepository _authRepository;
        public CreateUserHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<ResponseModel> Handle(UserCommand request, CancellationToken cancellationToken)
        {
            CreatePasswordHash(request.Model.Password, out byte[] hash, out byte[] salt);

            request.Model.PasswordHash = hash;
            request.Model.PasswordSalt = salt;
            return await _authRepository.RegisterUserAsync(request.Model);
        }
        private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();

            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
