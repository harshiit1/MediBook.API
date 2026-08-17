using MediatR;
using Medicare.Application.Features.Queries.Appointments;
using Medicare.Application.Interfaces.IAppointment;
using Medicare.Application.Models.Appointment;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetSpecialitiesQueryHandler
        : IRequestHandler<GetSpecialitiesQuery, List<SpecialityModel>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetSpecialitiesQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<List<SpecialityModel>> Handle(
            GetSpecialitiesQuery request, CancellationToken cancellationToken)
        {
            return await _appointmentRepository.GetSpecialitiesAsync(
                request.DoctorName, request.DepartmentName);
        }
    }
}
