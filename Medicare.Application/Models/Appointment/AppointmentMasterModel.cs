using System.Text.Json.Serialization;

namespace Medicare.Application.Models.Appointment
{
    public class AppointmentMasterModel
    {
        public int PatientId { get; set; }
        public int ProfileId { get; set; }
        public int AssociateId { get; set; }
        public int SlotId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int AgeTypeId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public int RelatonTypeId { get; set; }
        public string VisitPurpose { get; set; }
        public string VisitType { get; set; }
        public string OtpMethod { get; set; }
        public InsuranceData InsuranceData { get; set; }
        public PaymentData PaymentData { get; set; }
        public bool Insurance {  get; set; }
        public string CreatedBy { get; set; }
        public string AssociateRole { get; set; }
    }

    public class InsuranceData
    {
        public string? Provider { get; set; }
        public string? Policy { get; set; }
        public string? GroupId { get; set; }
        public string? HolderName { get; set; }
        public string? Address { get; set; }
    }

    public class PaymentData
    {
        public string? PaymentType { get; set; }
        public string? CardHolder { get; set; }
        public string? CardNumber { get; set; }
        public string? Expiry { get; set; }
        public string? CVV { get; set; }
        [JsonIgnore]
        public byte[]? CvvHash { get; set; }
        [JsonIgnore]
        public byte[]? CvvSalt { get; set; }
    }
}