using Medicare.Application.Models.Authentication;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.User;

namespace Medicare.Application.Interfaces.UserRepository
{
        public interface IUserRepository
        {
            Task<UserInfoDataModel> GetUserInfoAsync(string Username);
            Task<UserInfoDataModel> GetUserByIdAsync(int Id);
            Task<UserDetailModel> GetUserByEmailAsync(string email);
    }
}