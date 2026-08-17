using MediatR;
using Medicare.Application.Models.Appointment;

namespace Medicare.Application.Features.Queries.Appointments
{
    public record GetAppointmentByIdQuery(int appointmentId) : IRequest<AppointmentDetailModel>;
}
