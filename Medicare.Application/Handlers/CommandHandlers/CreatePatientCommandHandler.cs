using MediatR;
using Medicare.Application.Features.Commands.Patient;
using Medicare.Application.Interfaces.IPatient;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Patient;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, ResponseModel>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly PasswordHelper _passwordHelper;

        public CreatePatientCommandHandler(IPatientRepository patientRepository, PasswordHelper passwordHelper)
        {
            _patientRepository = patientRepository;
            _passwordHelper = passwordHelper;
        }

        public async Task<ResponseModel> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            string passwordHash = _passwordHelper.HashPassword(request.model.Password);

            string answerHash = _passwordHelper.HashPassword(request.model.SecurityAnswer);

            var patientModel = new PatientMasterModel
                {
                FirstName = request.model.FirstName,
                MiddleName = request.model.MiddleName,
                LastName = request.model.LastName,
                DateOfBirth = request.model.DateOfBirth,
                PhoneNumber = request.model.PhoneNumber,
                PhoneCountryCode = request.model.PhoneCountryCode,
                Email = request.model.Email,
                Gender = request.model.Gender,
                AddressLine1 = request.model.AddressLine1,
                AddressLine2 = request.model.AddressLine2,
                CityId = request.model.CityId,
                ZipCode = request.model.ZipCode,
                StateId = request.model.StateId,
                CountryId = request.model.CountryId,
                Username = request.model.Username,
                PasswordHash = passwordHash,
                SecurityQuestionId = request.model.SecurityQuestionId,
                SecurityAnswerHash = answerHash,   
                IsActive = request.model.IsActive,
                CreatedBy = request.model.CreatedBy,
            };

            return await _patientRepository.CreatePatientDetails(patientModel);
        }
    }
}
