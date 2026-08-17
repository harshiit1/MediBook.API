using MediatR;
using Medicare.Application.Features.Queries.Master;
using Medicare.Application.Interfaces.Master;
using Medicare.Application.Models.Master;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetRealtionTypeListQueryHandler : IRequestHandler<GetRealtionTypeListQuery, List<RelationTypeModel>>
    {
        private readonly IMasterRepository _masterRepository;
        public GetRealtionTypeListQueryHandler(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }
        public async Task<List<RelationTypeModel>> Handle(GetRealtionTypeListQuery request, CancellationToken cancellationToken)
        {
            return await _masterRepository.GetRelationTypeListAsync();
        }
    }
}
