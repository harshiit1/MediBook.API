using MediatR;
using Medicare.Application.Features.Queries.Patient;
using Medicare.Application.Interfaces.IPatient;
using Medicare.Application.Models.Appointment;
using Medicare.Application.Models.Patient;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, PatientDetailModel>
    {
        private readonly IPatientRepository _patientRepository;
        public GetPatientByIdQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public async Task<PatientDetailModel> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            return await _patientRepository.GetPatientById(request.Id);
        }
    }
}
