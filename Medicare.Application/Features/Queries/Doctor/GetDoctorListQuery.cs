using MediatR;
using Medicare.Application.Models.Doctor;

namespace Medicare.Application.Features.Queries.Doctor
{
    public record GetDoctorListQuery() : IRequest<List<DoctorCategoryModel>>;
}
