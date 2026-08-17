using MediatR;
using Medicare.Application.Features.Queries.Lab;
using Medicare.Application.Interfaces.ILab;
using Medicare.Application.Models.Lab;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetLabResultDetailByIdQueryHandler : IRequestHandler<GetLabResultDetailByIdQuery, LabResultSummaryModel>
    {
        private readonly ILabRepository _labRepository;
        public GetLabResultDetailByIdQueryHandler(ILabRepository labRepository) 
        {
            _labRepository = labRepository;
        }
        public async Task<LabResultSummaryModel> Handle(GetLabResultDetailByIdQuery request, CancellationToken cancellationToken)
        {
            return await _labRepository.GetLabResultDetailByIdAsync(request.id);
        }
    }
}
