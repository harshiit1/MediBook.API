using MediatR;
using Medicare.Application.Features.Queries.Lab;
using Medicare.Application.Interfaces.ILab;
using Medicare.Application.Models.Lab;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetLabResultDetailByProfileIdQueryHandler : IRequestHandler<GetLabResultsByProfileIdQuery, List<LabResultSummaryModel>>
    {
        private readonly ILabRepository _labRepository;
        public GetLabResultDetailByProfileIdQueryHandler(ILabRepository labRepository)
        {
            _labRepository = labRepository;
        }
        public async Task<List<LabResultSummaryModel>> Handle(GetLabResultsByProfileIdQuery request, CancellationToken cancellationToken)
        {
            return await _labRepository.GetLabResultDetailByProfileIdAsync(request.profileId);
        }
    }
}
