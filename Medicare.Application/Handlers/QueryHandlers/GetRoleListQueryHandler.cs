using MediatR;
using Medicare.Application.Features.Queries.Master;
using Medicare.Application.Interfaces.Master;
using Medicare.Application.Models.MasterModels;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetRoleListQueryHandler : IRequestHandler<GetRoleListQuery, List<RoleDataModel>>
    {
        private readonly IMasterRepository _masterRepository;

        public GetRoleListQueryHandler(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }
        public async Task<List<RoleDataModel>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var excludedRole = request.role switch
            {
                "Admin" => new[] { 1, 2 },
                _       => new[] { 1,2 } 
            };

            var result = await _masterRepository.GetRoleListAsync();

            return result.Where(r => !excludedRole.Contains(r.RoleId)).ToList();
        }
    }
}
