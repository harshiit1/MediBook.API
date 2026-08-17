using Medicare.Application.Interfaces.IErrorHandling;

namespace Medicare.Application.Models.Authentication
{
    public class AuthModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AuthDetailModel
    {
        public int UserId { get; set; }
        public string PasswordHash { get; set; }
        public string UserType { get; set; }
    }
    
    public class AuthResultModel : IErrorHandling
    {
        public Guid UserId { get; set; }
        public int RefId { get; set; }        // PatientId or AssociateId
        public string UserType { get; set; }  // "Patient" or "Associate"
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }

        public Guid? TenantId { get; set; }
        public string? EmployeeId { get; set; }
        public string? DepartmentName { get; set; }
        public string? DesignationName { get; set; }

        public int IsSuccess { get; set; }
        public int Status { get; set; }
        public string ResponseMessage { get; set; }
    }
}
