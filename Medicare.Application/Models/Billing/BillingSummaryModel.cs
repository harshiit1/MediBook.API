using Medicare.Application.Interfaces.IErrorHandling;

namespace Medicare.Application.Models.Billing
{
    public class BillingSummaryModel : IErrorHandling
    {
        public int BillId { get; set; }
        public int ProfileId { get; set; }
        public int PatientId { get; set; }
        public string DoctorName { get; set; } 
        public string ClinicName { get; set; } 
        public DateOnly VisitDate { get; set; }
        public DateOnly OrderDate { get; set; }
        public DateOnly? PaymentDate { get; set; }
        public int TotalCharge { get; set; }
        public int InsuranceCovered { get; set; }
        public int Adjustments { get; set; }
        public int PatientResponsibility { get; set; }
        public int RemainingBalance { get; set; }
        public string ResponseMessage { get; set; }
        public int IsSuccess { get; set; }
        public int Status { get; set; }
    }
}
