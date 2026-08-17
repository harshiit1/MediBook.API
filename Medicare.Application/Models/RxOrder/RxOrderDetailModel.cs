using Medicare.Application.Interfaces.IErrorHandling;

namespace Medicare.Application.Models.Orders
{
    public class RxOrderDetailModel : IErrorHandling
    {
        public int OrderId { get; set; }
        public int PatientId { get; set; }
        public int ProfileId { get; set; }
        public int AssociateId { get; set; }
        public string PatientName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string DoctorName { get; set; }
        public int? PharmacyId { get; set; }
        public string? PharmacyName { get; set; }
        public string? PharmacistName { get; set; }
        public string? PharmacyMobile { get; set; }
        public string? PharmacyAddress { get; set; }
        public string DrugName { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }   
        public int DurationDays { get; set; }
        public string Instructions { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string OrderStatus { get; set; }
        public string? CancelReason { get; set; }
        public DateTime CancelledDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int IsSuccess { get; set; }
        public int Status { get; set; }
        public string ResponseMessage { get; set; }
    }
}
