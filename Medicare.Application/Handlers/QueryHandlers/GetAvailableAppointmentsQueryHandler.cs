using MediatR;
using Medicare.Application.Features.Queries.Appointments;
using Medicare.Application.Interfaces.IAppointment;
using Medicare.Application.Models.Appointment;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetAvailableAppointmentsQueryHandler
        : IRequestHandler<GetAvailableAppointmentsQuery, List<AvailableAppointmentModel>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAvailableAppointmentsQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<List<AvailableAppointmentModel>> Handle(
            GetAvailableAppointmentsQuery request, CancellationToken cancellationToken)
        {
            return await _appointmentRepository.GetAvailableAppointmentsAsync(request.associateId);
        }
    }
}
