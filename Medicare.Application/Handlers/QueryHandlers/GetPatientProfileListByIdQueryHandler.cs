using MediatR;
using Medicare.Application.Features.Queries.Patient;
using Medicare.Application.Interfaces.IPatient;
using Medicare.Application.Models.Patient;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetPatientProfileListByIdQueryHandler : IRequestHandler<GetPatientProfileListByIdQuery, List<PatientProfileModel>>
    {
        private readonly IPatientRepository _patientRepository;
        public GetPatientProfileListByIdQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public async Task<List<PatientProfileModel>> Handle(GetPatientProfileListByIdQuery request, CancellationToken cancellationToken)
        {
            return await _patientRepository.GetPatientProfileListByIdAsync(request.patientId);
        }
    }
}
