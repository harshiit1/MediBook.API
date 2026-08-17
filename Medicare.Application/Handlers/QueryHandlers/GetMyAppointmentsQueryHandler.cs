using MediatR;
using Medicare.Application.Features.Queries.Appointments;
using Medicare.Application.Interfaces.IAppointment;
using Medicare.Application.Models.Appointment;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetMyAppointmentsQueryHandler
       : IRequestHandler<GetMyAppointmentsQuery, List<PatientAppointmentModel>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public GetMyAppointmentsQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<List<PatientAppointmentModel>> Handle(
            GetMyAppointmentsQuery request, CancellationToken cancellationToken)
        {
            return await _appointmentRepository.GetMyAppointmentsAsync(request.PatientId);
        }
    }
}
