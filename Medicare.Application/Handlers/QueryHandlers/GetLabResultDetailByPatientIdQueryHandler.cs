using MediatR;
using Medicare.Application.Features.Queries.Lab;
using Medicare.Application.Interfaces.ILab;
using Medicare.Application.Models.Lab;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetLabResultDetailByPatientIdQueryHandler : IRequestHandler<GetLabResultsByPatientIdQuery, List<LabResultSummaryModel>>
    {
        private readonly ILabRepository _labRepository;
        public GetLabResultDetailByPatientIdQueryHandler(ILabRepository labRepository)
        {
            _labRepository = labRepository;
        }
        public async Task<List<LabResultSummaryModel>> Handle(GetLabResultsByPatientIdQuery request, CancellationToken cancellationToken)
        {
            return await _labRepository.GetLabResultDetailByPatientIdAsync(request.patientId);
        }
    }
}
