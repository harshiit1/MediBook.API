using MediatR;
using Medicare.Application.Features.Commands.Patient;
using Medicare.Application.Interfaces.IPatient;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.Patient;
using System.Security.Cryptography;
using System.Text;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, ResponseModel>
    {
        private readonly IPatientRepository _patientRepository;
        public UpdatePatientCommandHandler(IPatientRepository patientRepository) 
        {
            _patientRepository = patientRepository; 
        }
        public async Task<ResponseModel> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var patientModel = new UpdatePatientRequestModel
            {
                PatientId = request.model.PatientId,
                FirstName = request.model.FirstName,
                MiddleName = request.model.MiddleName,
                LastName = request.model.LastName,
                DateOfBirth = request.model.DateOfBirth,
                PhoneCountryCode = request.model.PhoneCountryCode,
                PhoneNumber = request.model.PhoneNumber,
                Email = request.model.Email,
                Gender = request.model.Gender,
                AddressLine1 = request.model.AddressLine1,
                AddressLine2 = request.model.AddressLine2,
                CityId = request.model.CityId,
                ZipCode = request.model.ZipCode,
                StateId = request.model.StateId,
                CountryId = request.model.CountryId,
                IsActive = request.model.IsActive,
                UpdatedDate = request.model.UpdatedDate
            };

            return await _patientRepository.UpdatePatientDetails(patientModel);
        }
    }
}
