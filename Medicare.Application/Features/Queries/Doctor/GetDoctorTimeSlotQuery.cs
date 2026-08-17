using MediatR;
using Medicare.Application.Models.Doctor;

namespace Medicare.Application.Features.Queries.Doctor
{
    public record GetDoctorTimeSlotQuery(DoctorTimeSlotRequestModel model) : IRequest<List<DoctorAvailabilityModel>>;
}
