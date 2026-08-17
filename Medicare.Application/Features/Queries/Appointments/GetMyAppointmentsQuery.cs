using MediatR;
using Medicare.Application.Models.Appointment;

namespace Medicare.Application.Features.Queries.Appointments
{
    public record GetMyAppointmentsQuery(int PatientId)
       : IRequest<List<PatientAppointmentModel>>;
}
