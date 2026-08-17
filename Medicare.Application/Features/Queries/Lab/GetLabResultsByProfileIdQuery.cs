using MediatR;
using Medicare.Application.Models.Lab;

namespace Medicare.Application.Features.Queries.Lab
{
    public record GetLabResultsByProfileIdQuery(int profileId) : IRequest<List<LabResultSummaryModel>>;
}
