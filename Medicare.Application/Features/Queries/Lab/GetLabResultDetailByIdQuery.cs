using MediatR;
using Medicare.Application.Models.Lab;

namespace Medicare.Application.Features.Queries.Lab
{
    public record GetLabResultDetailByIdQuery(int id) : IRequest<LabResultSummaryModel>;
}
