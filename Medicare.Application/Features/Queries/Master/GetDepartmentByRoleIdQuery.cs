using MediatR;
using Medicare.Application.Models.Master;

namespace Medicare.Application.Features.Queries.Master
{
    public record GetDepartmentByRoleIdQuery(int roleId) : IRequest<List<DepartmentDataModel>>;
}
