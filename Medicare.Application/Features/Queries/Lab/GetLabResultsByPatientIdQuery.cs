using MediatR;
using Medicare.Application.Models.Lab;

namespace Medicare.Application.Features.Queries.Lab
{
    public record GetLabResultsByPatientIdQuery(int patientId) : IRequest<List<LabResultSummaryModel>>;
}
