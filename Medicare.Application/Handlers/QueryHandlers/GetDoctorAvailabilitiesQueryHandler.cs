using MediatR;
using Medicare.Application.Features.Queries.Doctor;
using Medicare.Application.Interfaces.IDoctor;
using Medicare.Application.Models.Doctor;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetDoctorAvailabilitiesQueryHandler
       : IRequestHandler<GetDoctorAvailabilitiesQuery, List<DoctorAvailabilityModel>>
    {
        private readonly IDoctorRepository _doctorRepository;

        public GetDoctorAvailabilitiesQueryHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<List<DoctorAvailabilityModel>> Handle(
            GetDoctorAvailabilitiesQuery request, CancellationToken cancellationToken)
        {
            return await _doctorRepository.GetDoctorAvailabilitiesAsync(request.DoctorId);
        }
    }
}
