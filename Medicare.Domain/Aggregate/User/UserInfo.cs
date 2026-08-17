namespace Medicare.Domain.Aggregate.User
{
    public class UserInfo
    {
        public UserInfo()
        {

        }
        public string FirstName { get; private set; }
        public string? MiddleName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string PhoneNumber { get; private set; }
        public string EmailId { get; private set; }
        public string? Gender { get; private set; }
        public string AddressLine1 { get; private set; }
        public string? AddressLine2 { get; private set; }
        public string City { get; private set; }
        public string ZipPostalCode { get; private set; }
        public string Country { get; private set; }
        public string State { get; private set; }
        public string SecurityQuestion { get; private set; }
        public string SecurityAnswer { get; private set; }
        public bool IsActive { get; private set; }

        public static UserInfo CreateUserInfo( 
            string firstName,
            string? middleName,
            string lastName,
            DateTime dateOfBirth,
            string phoneNumber,
            string emailId,
            string? gender,
            string addressLine1,
            string? addressLine2,
            string city,
            string zipPostalCode,
            string country,
            string state,
            string securityQuestion,
            string securityAnswer)
        { 
            return new UserInfo
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                PhoneNumber = phoneNumber,
                EmailId = emailId,
                Gender = gender,
                AddressLine1 = addressLine1,
                AddressLine2 = addressLine2,
                City = city,
                ZipPostalCode = zipPostalCode,
                Country = country,
                State = state,
                SecurityQuestion = securityQuestion,
                SecurityAnswer = securityAnswer,
                IsActive = true
            };
        }
    }
}
