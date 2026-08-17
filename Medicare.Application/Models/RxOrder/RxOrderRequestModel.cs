namespace Medicare.Application.Models.Orders
{
    public class CreateRxOrderRequestModel
    {
        public int PatientId { get; set; }
        public int ProfileId { get; set; }
        public int AssociateId { get; set; }
        public int? PharmacyId { get; set; }
        public string DrugName { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public int DurationDays { get; set; }
        public string Instructions { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }

    public class UpdateRxOrderRequestModel
    {
        public int OrderId { get; set; }

        public int PatientId { get; set; }
        public int? PharmacyId { get; set; }
        public string? DrugName { get; set; }
        public string? Dosage { get; set; }
        public string? Frequency { get; set; }
        public int? DurationDays { get; set; }
        public string? Instructions { get; set; }
        public int? Status { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }

    public class CancelRxOrderRequestModel
    {
        public int OrderId { get; set; }
        public int PatientId { get; set; }
        public string CancelReason { get; set; }
    }
}
