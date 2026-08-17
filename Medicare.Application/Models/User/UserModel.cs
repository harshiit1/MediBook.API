using Medicare.Application.Interfaces.IErrorHandling;
using System.Text.Json.Serialization;

namespace Medicare.Application.Models.User
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public byte[]? PasswordHash { get; set; }
        [JsonIgnore]
        public byte[]? PasswordSalt { get; set; }
        public int RoleId { get; set; }
        public int DepartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CreatedBy { get; set; }
    }
    public class UserInfoDataModel : IErrorHandling
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string? Gender { get; set; }
        //public string AddressLine1 { get; set; }
        //public string? AddressLine2 { get; set; }
        //public string City { get; set; }
        //public string ZipPostalCode { get; set; }
        //public string Country { get; set; }
        //public string State { get; set; }
        //public string SecurityQuestion { get; set; }
        //public string SecurityAnswer { get; set; }
        public bool IsActive { get; set; }
        public int IsSuccess { get; set; }
        public int Status { get; set; }
        public string ResponseMessage { get; set; }
    }

    public class SecurityQuestionDataModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
    }
}
