using MediatR;
using Medicare.Application.Features.Queries.Billing;
using Medicare.Application.Interfaces.IBilling;
using Medicare.Application.Models.Billing;

namespace Medicare.Application.Handlers.QueryHandlers
{
    public class GetBillsByPatientIdQueryHandler : IRequestHandler<GetBillsByPatientIdQuery, List<BillingSummaryModel>>
    {
        private readonly IBillingRepository _billingRepository;
        public GetBillsByPatientIdQueryHandler(IBillingRepository billingRepository)
        {
            _billingRepository = billingRepository;
        }
        public async Task<List<BillingSummaryModel>> Handle(GetBillsByPatientIdQuery request, CancellationToken cancellationToken)
        {
            return await _billingRepository.GetBillsByPatientIdAsync(request.patientId);
        }
    }
}
