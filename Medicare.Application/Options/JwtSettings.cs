namespace Medicare.Application.Options
{
    public class JwtSettings
    {
        public string SigningKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public double TokenExpiryMinutes { get; set; } 
    }
}
