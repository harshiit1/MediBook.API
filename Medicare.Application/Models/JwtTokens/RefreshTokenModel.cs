using Medicare.Application.Interfaces.IErrorHandling;

namespace Medicare.Application.Models.JwtTokens
{
    public  class RefreshTokenRequestModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
    public class RefreshTokenResponseModel : IErrorHandling
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string ResponseMessage { get; set; }
        public int IsSuccess { get; set; }
    }
    public class RefreshTokenDto
    {
        public Guid UserId { get; set; }
        public string UserType { get; set; }
        public bool IsSuccess { get; set; }
        public string ResponseMessage { get; set; }
    }

}
