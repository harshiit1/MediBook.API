using MediatR;
using Medicare.Application.Models.Appointment;

namespace Medicare.Application.Features.Queries.Appointments
{
    public record GetAvailableAppointmentsQuery(int associateId) : IRequest<List<AvailableAppointmentModel>>;
}
