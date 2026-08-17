using MediatR;
using Medicare.Application.Features.Commands.Patient;
using Medicare.Application.Interfaces.IPatient;
using Medicare.Application.Models.Patient;
using System.Security.Cryptography;
using System.Text;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class AuthPatientCommandHandler : IRequestHandler<AuthPatientCommand, PatientDetailModel>
    {
        private readonly IPatientRepository _patientRepository;

        public AuthPatientCommandHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PatientDetailModel> Handle(AuthPatientCommand request, CancellationToken cancellationToken)
        {
            var user = await _patientRepository.GetPasswordByUsernameAsync(request.model.Username);

            if (user.PasswordHash == null && user.PasswordSalt == null)
            {
                return new PatientDetailModel
                {
                    IsSuccess = 0,
                    ResponseMessage = "User not found."
                };
            }

            bool isPasswordValid = VerifyPassword(request.model.Password, user.PasswordHash, user.PasswordSalt);

            if (!isPasswordValid)
            {
                return new PatientDetailModel
                {
                    IsSuccess = 0,
                    ResponseMessage = "Invalid password."
                };
            }

            var result = await _patientRepository.GetPatientInfoByUsername(request.model.Username);

            return result;
        }
        private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(storedHash);
        }
    }
}
