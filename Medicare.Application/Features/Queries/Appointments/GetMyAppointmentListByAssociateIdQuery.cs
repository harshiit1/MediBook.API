using MediatR;
using Medicare.Application.Models.Patient;

namespace Medicare.Application.Features.Queries.Appointments
{
    public record GetMyAppointmentListByAssociateIdQuery(int associateId) : IRequest<List<PatientProfileModel>>;
}
