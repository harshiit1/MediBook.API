using MediatR;
using Medicare.Application.Features.Commands.ErrorLog;
using Medicare.Application.Interfaces.IErrorLog;
using Medicare.Application.Models.CommonModels.ErrorLog;
using Medicare.Application.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class CreateErrorLogHandler : IRequestHandler<ErrorLogCommand, ErrorLogModel>
    {
        private readonly IErrorLogRepository _errorLogRepository;
        public CreateErrorLogHandler(IErrorLogRepository errorLogRepository)
        {
            _errorLogRepository = errorLogRepository;
        }
        public async Task<ErrorLogModel> Handle(ErrorLogCommand request, CancellationToken cancellationToken)
        {
            return await _errorLogRepository.InsertErrorLog(request.Model);
        }
    }
}
