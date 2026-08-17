using MediatR;
using Medicare.Application.Features.Queries.ErrorLog;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Models.CommonModels.ErrorLog;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetErrorLogHandler : IRequestHandler<GetErrorLogQuery, List<ErrorLogModel>>
    {
        private readonly IErrorLogRepository _errorLogRepository;
        public GetErrorLogHandler(IErrorLogRepository errorLogRepository)
        {
            _errorLogRepository = errorLogRepository;
        }
        public async Task<List<ErrorLogModel>> Handle(GetErrorLogQuery request, CancellationToken cancellationToken)
        {
            return await _errorLogRepository.GetErrorLogQuery();
        }
    }
}
