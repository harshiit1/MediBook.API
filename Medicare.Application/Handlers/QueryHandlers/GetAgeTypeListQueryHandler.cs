using MediatR;
using Medicare.Application.Features.Queries.Master;
using Medicare.Application.Interfaces.Master;
using Medicare.Application.Models.Master;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetAgeTypeListQueryHandler : IRequestHandler<GetAgeTypeListQuery, List<AgeTypeModel>>
    {
        private readonly IMasterRepository _masterRepository;
        public GetAgeTypeListQueryHandler(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }
        public async Task<List<AgeTypeModel>> Handle(GetAgeTypeListQuery request, CancellationToken cancellationToken)
        {
            return await _masterRepository.GetAgeTypeListAsync();
        }
    }
}
