using Medicare.Domain.Aggregate.User;

namespace Medicare.Domain.Aggregate.AuthUser
{
    public class UserAggregate
    {
        private UserAggregate() 
        { 
        }
        public int UserId { get; private set; }
        public UserInfo UserInfo { get; private set; }
        public AuthInfo AuthInfo { get; private set; }
        public DateTime CreatedOn { get; private set; }
 
        public static UserAggregate Create(UserInfo userInfo)
        {
            //TO DO: add validation, error handling & error notifs strategies
            return new UserAggregate
            {
                UserInfo = userInfo,
                CreatedOn = DateTime.Now
            };
        }
    }
}
