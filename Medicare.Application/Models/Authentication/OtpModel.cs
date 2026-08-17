using Medicare.Application.Interfaces.IErrorHandling;
using System.Text.Json.Serialization;

namespace Medicare.Application.Models.Authentication
{
    public class OtpDetailModel : IErrorHandling
    {
        public Guid UserId { get; set; }
        public string UserType { get; set; } 
        //public string FullName { get; set; } 
        public string Email { get; set; }
        [JsonIgnore]
        public string OtpHash { get; set; }  
        public DateTime OtpExpiry { get; set; }
        public int OtpAttempts { get; set; } = 0;
        public string ResponseMessage { get; set; }
        public int IsSuccess { get; set; }
    }
    public class RequestOtpModel
    {
        public string Email { get; set; }
    }

    public class VerifyOtpModel
    {
        public string Email { get; set; }
        public string OtpCode { get; set; }
    }
}
