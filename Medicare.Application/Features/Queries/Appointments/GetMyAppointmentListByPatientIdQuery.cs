using MediatR;
using Medicare.Application.Models.Appointment;

namespace Medicare.Application.Features.Queries.Appointments
{
    public record GetMyAppointmentListByPatientIdQuery(int PatientId)
       : IRequest<List<PatientAppointmentModel>>;
}
