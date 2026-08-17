using MediatR;
using Medicare.Application.Models.Appointment;

namespace Medicare.Application.Features.Queries.Appointments
{
    public record GetSpecialitiesQuery(
        string? DoctorName,
        string? DepartmentName
    ) : IRequest<List<SpecialityModel>>;
}
