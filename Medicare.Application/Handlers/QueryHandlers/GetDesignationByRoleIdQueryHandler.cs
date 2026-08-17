using MediatR;
using Medicare.Application.Features.Queries.Master;
using Medicare.Application.Interfaces.Master;
using Medicare.Application.Models.Master;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetDesignationByRoleIdQueryHandler : IRequestHandler<GetDesignationByRoleIdQuery, List<DesignationDataModel>>
    {
        private readonly IMasterRepository _masterRepository;
        public GetDesignationByRoleIdQueryHandler(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }

        public async Task<List<DesignationDataModel>> Handle(GetDesignationByRoleIdQuery request, CancellationToken cancellationToken)
        {
            return await _masterRepository.GetDesignationByRoleIdAsync(request.roleId);
        }
    }
}
