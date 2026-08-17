using MediatR;
using Medicare.Application.Features.Queries.Master;
using Medicare.Application.Interfaces.Master;
using Medicare.Application.Models.Master;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetDepartmentByRoleIdQueryHandler : IRequestHandler<GetDepartmentByRoleIdQuery, List<DepartmentDataModel>>
    {
        private readonly IMasterRepository _masterRepository;

        public GetDepartmentByRoleIdQueryHandler(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }
        public async Task<List<DepartmentDataModel>> Handle(GetDepartmentByRoleIdQuery request, CancellationToken cancellationToken)
        {
            return await _masterRepository.GetDepartmentByRoleIdAsync(request.roleId);
        }
    }
}
