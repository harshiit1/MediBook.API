using MediatR;
using Medicare.Application.Features.Queries.Master;
using Medicare.Application.Interfaces.Master;
using Medicare.Application.Models.Master;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetStatusKeyQueryHandler : IRequestHandler<GetStatusKeyListQuery, List<StatusCategoryModel>>
    {
        private readonly IMasterRepository _masterRepository;
        public GetStatusKeyQueryHandler(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }
        public async Task<List<StatusCategoryModel>> Handle(GetStatusKeyListQuery request, CancellationToken cancellationToken)
        {
            var result = await _masterRepository.GetStatusListAsync();

            return result
                .GroupBy(x => x.Category)
                .Select(g => new StatusCategoryModel
                {
                    Category = g.Key,
                    Statuses = g.Select(s => new StatusModel
                    {
                        StatusId = s.StatusId,
                        StatusKey = s.StatusKey,
                        StatusValue = s.StatusValue
                    }).ToList()
                }).ToList();
        }
    }
}
