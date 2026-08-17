using MediatR;
using Medicare.Application.Features.Queries.Doctor;
using Medicare.Application.Interfaces.IDoctor;
using Medicare.Application.Models.Doctor;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetDoctorTimeSlotQueryHandler : IRequestHandler<GetDoctorTimeSlotQuery, List<DoctorAvailabilityModel>>
    {
        private readonly IDoctorRepository _doctorRepository;
        public GetDoctorTimeSlotQueryHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<List<DoctorAvailabilityModel>> Handle(GetDoctorTimeSlotQuery request, CancellationToken cancellationToken)
        {
            return await _doctorRepository.GetDoctorTimeSlotsAsync(request.model);
        }
    }
}
