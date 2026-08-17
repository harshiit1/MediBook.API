using MediatR;
using Medicare.Application.Features.Queries.Master;
using Medicare.Application.Interfaces.Master;
using Medicare.Application.Models.MasterModels;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetWeekDaysListQueryHandler : IRequestHandler<GetWeekDaysListQuery, List<WeekDaysModel>>
    {
        private readonly IMasterRepository _masterRepository;
        public GetWeekDaysListQueryHandler(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }
        public async Task<List<WeekDaysModel>> Handle(GetWeekDaysListQuery request, CancellationToken cancellationToken)
        {
            return await _masterRepository.GetWeekDaysListAsync();
        }
    }
}
