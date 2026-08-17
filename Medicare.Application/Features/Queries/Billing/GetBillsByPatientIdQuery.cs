using MediatR;
using Medicare.Application.Models.Billing;

namespace Medicare.Application.Features.Queries.Billing
{
    public record GetBillsByPatientIdQuery(int patientId) : IRequest<List<BillingSummaryModel>>;
}
