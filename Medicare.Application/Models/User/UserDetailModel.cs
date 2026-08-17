using Medicare.Application.Interfaces.IErrorHandling;

namespace Medicare.Application.Models.User
{
    public class UserDetailModel : IErrorHandling
    {
        public Guid UserId { get; set; }
        public string UserType { get; set; } 
        public string FullName { get; set; } 
        public string Email { get; set; } 
        public int Status { get; set; }
        public int IsSuccess { get; set; }
        public string ResponseMessage { get; set; } 
    }
}
