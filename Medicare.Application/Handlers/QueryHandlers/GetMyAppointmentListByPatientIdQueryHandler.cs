using MediatR;
using Medicare.Application.Features.Queries.Appointments;
using Medicare.Application.Interfaces.IAppointment;
using Medicare.Application.Models.Appointment;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetMyAppointmentListByPatientIdQueryHandler
       : IRequestHandler<GetMyAppointmentListByPatientIdQuery, List<PatientAppointmentModel>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public GetMyAppointmentListByPatientIdQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<List<PatientAppointmentModel>> Handle(
            GetMyAppointmentListByPatientIdQuery request, CancellationToken cancellationToken)
        {
            return await _appointmentRepository.GetMyAppointmentListByPatientIdAsync(request.PatientId);
        }
    }
}
