using Medicare.Application.Models.Billing;

namespace Medicare.Application.Interfaces.IBilling
{
    public interface IBillingRepository
    {
        Task<BillingSummaryModel> GetBillsByIdAsync(int id);
        Task<List<BillingSummaryModel>> GetBillsByPatientIdAsync(int patientId);
    }
}
