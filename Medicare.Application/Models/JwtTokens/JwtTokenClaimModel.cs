namespace Medicare.Application.Models.JwtTokens
{
    public class JwtTokenClaimModel
    {
        public Guid UserId { get; set; }   
        public int RefId { get; set; }   
        public string UserType { get; set; }   
        public string Email { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public Guid? TenantId { get; set; }
    }
    public class JwtRefreshTokenModel
    {
        public Guid UserId { get; set; }
        public string UserType { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
