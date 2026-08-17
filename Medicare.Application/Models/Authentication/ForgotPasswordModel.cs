using Medicare.Application.Interfaces.IErrorHandling;

namespace Medicare.Application.Models.Authentication
{
    public class ResetForgotPasswordModel
    {
        public Guid Token { get; set; }
        public string Password { get; set; }
    }
    public class VerifyForgotPasswordModel
    {
        public string Email { get; set; }
        public string OtpCode { get; set; }
    }
    public class VerifyForgotPasswordResponseModel : IErrorHandling
    {
        public Guid? Token { get; set; }
        public int IsSuccess { get; set; }
        public int Status { get; set; }
        public string ResponseMessage { get; set; }
    }
}
