using MediatR;
using Medicare.Application.Models.Speciality;

namespace Medicare.Application.Features.Queries.Master
{
    public record GetSpecialityByDepartmentIdQuery(int departmentId) : IRequest<List<SpecialityTypeModel>>;
}
