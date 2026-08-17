using MediatR;
using Medicare.Application.Features.Commands.Authentication;
using Medicare.Application.Interfaces.IAssociate;
using Medicare.Application.Interfaces.IAuthRepository;
using Medicare.Application.Interfaces.IPatient;
using Medicare.Application.Models.Authentication;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class AuthCommandHandler : IRequestHandler<AuthCommand, AuthResultModel>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IAssociateRepository _associateRepository;
        private readonly PasswordHelper _passwordHelper;
        public AuthCommandHandler(IAuthRepository authRepository, PasswordHelper passwordHelper, IPatientRepository patientRepository, IAssociateRepository associateRepository)
        {
            _authRepository = authRepository;
            _passwordHelper = passwordHelper;
            _patientRepository = patientRepository;
            _associateRepository = associateRepository;
        }
        public async Task<AuthResultModel> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var userLoginCreds = await _authRepository.GetPasswordByUsernameAsync(request.model.Username);

            if (userLoginCreds.PasswordHash == null)
            {
                return new AuthResultModel
                {
                    IsSuccess = 0,
                    Status = 0,
                    ResponseMessage = "User not found."
                };
            }

            bool isPasswordValid = _passwordHelper.VerifyPassword(request.model.Password, userLoginCreds.PasswordHash);

            if (!isPasswordValid)
            {
                return new AuthResultModel
                {
                    IsSuccess = 0,
                    Status = 0,
                    ResponseMessage = "Invalid password."
                };
            }
            if (userLoginCreds.UserType == "Patient")
            {
                var result = await _patientRepository.GetPatientInfoByUsername(request.model.Username);
                
                return new AuthResultModel
                {
                    IsSuccess = 1,
                    Status = 1,
                    ResponseMessage = "Login Successful",
                    UserId = result.UserId,
                    RefId = result.PatientId,
                    UserType = result.UserType,
                    RoleName = result.RoleName,
                    FullName = $"{result.FirstName} {result.MiddleName} {result.LastName}".Trim(),
                    Email = result.Email,
                    Username = result.Username,  
                    TenantId = null
                };
            }
            if (userLoginCreds.UserType == "Associate")
            {
                var result = await _associateRepository.GetAssociateInfoByUsername(request.model.Username);

                return new AuthResultModel
                {
                    IsSuccess = 1,
                    Status = 1,
                    ResponseMessage = "Login Successful",
                    UserId = result.UserId,
                    RefId = result.AssociateId,
                    UserType = result.UserType,
                    RoleName = result.RoleName,
                    FullName = $"{result.FirstName} {result.MiddleName} {result.LastName}".Trim(),
                    Email = result.EmailId,
                    EmployeeId = result.EmployeeId,
                    DepartmentName = result.DepartmentName,
                    DesignationName = result.DesignationName,
                    TenantId = result.TenantId  
                };
            }
            else
            {
                return new AuthResultModel
                {
                    Status = 0,
                    IsSuccess = 0,
                    ResponseMessage = "Account does not exist."
                };
            }
        }
    }
}
