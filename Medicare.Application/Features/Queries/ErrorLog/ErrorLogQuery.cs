using MediatR;
using Medicare.Application.Models.CommonModels.ErrorLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medicare.Application.Features.Queries.ErrorLog
{
    public record GetErrorLogQuery() : IRequest<List<ErrorLogModel>>;
}
