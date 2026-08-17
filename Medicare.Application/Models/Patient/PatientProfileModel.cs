using Medicare.Application.Interfaces.IErrorHandling;
using Medicare.Application.Models.Appointment;

namespace Medicare.Application.Models.Patient
{
    public class PatientProfileModel : IErrorHandling
    {
        public int ProfileId { get; set; }
        public int PatientId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? DateOfBirth { get; set; }
        public int Age { get; set; }
        public int AgeTypeId { get; set; }
        public string? AgeTypeName { get; set; }   
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int RelationTypeId { get; set; }
        public string? RelationTypeName { get; set; }   // "Self" | "Spouse" | "Child" etc.
        public InsuranceData Insurance { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int IsSuccess { get; set; }
        public int Status { get; set; }
        public string ResponseMessage { get; set; }
    }
    public class PatientProfileModelDto : IErrorHandling
    {
        public int ProfileId { get; set; }
        public int PatientId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? DateOfBirth { get; set; }
        public int Age { get; set; }
        public int AgeTypeId { get; set; }
        public string? AgeTypeName { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int RelationTypeId { get; set; }
        public string? RelationTypeName { get; set; }   // "Self" | "Spouse" | "Child" etc.
        public string? Provider { get; set; }
        public string? Policy { get; set; }
        public string? GroupId { get; set; }
        public string? HolderName { get; set; }
        public string? Address { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int IsSuccess { get; set; }
        public int Status { get; set; }
        public string ResponseMessage { get; set; }
    }
}
