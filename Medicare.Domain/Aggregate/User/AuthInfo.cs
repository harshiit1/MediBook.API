namespace Medicare.Domain.Aggregate.AuthUser
{
    public class AuthInfo
    {
        public AuthInfo() { }

        public string Username { get; set; }
        public string Password { get; set; }

        public static AuthInfo Create(string userName, string password)
        {
            return new AuthInfo
            {
                Username = userName,
                Password = password
            };
        }
    }
}