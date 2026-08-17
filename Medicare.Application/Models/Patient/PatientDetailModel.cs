using Medicare.Application.Interfaces.IErrorHandling;

namespace Medicare.Application.Models.Patient
{
    public class PatientDetailModel : IErrorHandling
    {
        public Guid UserId { get; set; }
        public int PatientId { get; set; }
        public int ProfileId { get; set; }
        public string UserType { get; set; } = "Patient";
        public string? RoleName { get; set; } = "Patient";
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Gender { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int CityId { get; set; }
        public string ZipCode { get; set; }
        public int? StateId { get; set; }
        public int CountryId { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int IsSuccess { get; set; }
        public int Status { get; set; }
        public string ResponseMessage { get; set; }
    }
}
