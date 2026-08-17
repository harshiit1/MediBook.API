using MediatR;
using Medicare.Application.Features.Queries.Patient;
using Medicare.Application.Interfaces.IPatient;
using Medicare.Application.Models.Patient;
using Medicare.Application.Models.Appointment;
namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetPatientProfileByProfileIdQueryHandler : IRequestHandler<GetPatientProfileByProfileIdQuery, PatientProfileModel>
    {
        private readonly IPatientRepository _patientRepository;
        public GetPatientProfileByProfileIdQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public async Task<PatientProfileModel> Handle(GetPatientProfileByProfileIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _patientRepository.GetPatientProfileByProfileIdAsync(request.profileId);
            return new PatientProfileModel
            {
                PatientId = result.PatientId,
                ProfileId = result.ProfileId,
                FirstName = result.FirstName,
                LastName = result.LastName,
                FullName = result.FullName,
                DateOfBirth = result.DateOfBirth,
                PhoneNumber = result.PhoneNumber,
                Email = result.Email,
                Gender = result.Gender,
                Age = result.Age,
                AgeTypeId = result.AgeTypeId,
                AgeTypeName = result.AgeTypeName,
                RelationTypeId = result.RelationTypeId,
                RelationTypeName = result.RelationTypeName,
                Insurance = new InsuranceData
                {
                    HolderName = result.HolderName,
                    Policy = result.Policy,
                    Provider = result.Provider,
                    GroupId = result.GroupId,
                    Address = result.Address
                },
                IsActive = result.IsActive,
                CreatedDate = result.CreatedDate,
                UpdatedDate = result.UpdatedDate,

                IsSuccess = result.IsSuccess,
                Status = result.Status,
                ResponseMessage = result.ResponseMessage
            };
        }
    }
}
