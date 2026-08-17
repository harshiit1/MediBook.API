using MediatR;
using Medicare.Application.Models.Billing;

namespace Medicare.Application.Features.Queries.Billing
{
    public record GetBillsByIdQuery(int id) : IRequest<BillingSummaryModel>;
}
