using MediatR;
using Medicare.Application.Features.Queries.Billing;
using Medicare.Application.Interfaces.IBilling;
using Medicare.Application.Models.Billing;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetBillsByIdQueryHandler : IRequestHandler<GetBillsByIdQuery, BillingSummaryModel>
    {
        private readonly IBillingRepository _billingRepository;
        public GetBillsByIdQueryHandler(IBillingRepository billingRepository)
        {
            _billingRepository = billingRepository;
        }
        public async Task<BillingSummaryModel> Handle(GetBillsByIdQuery request, CancellationToken cancellationToken)
        {
            return await _billingRepository.GetBillsByIdAsync(request.id);
        }
    }
}
