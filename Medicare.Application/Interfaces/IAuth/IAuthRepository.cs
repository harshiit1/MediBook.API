using Medicare.Application.Models.Associate;
using Medicare.Application.Models.Authentication;
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.User;

namespace Medicare.Application.Interfaces.IAuthRepository
{
    public interface IAuthRepository
    {
        Task<ResponseModel> RegisterUserAsync(UserModel Model);
        Task<AssociateResponseModel> RegisterAssociateAsync(CreateAssociateRequestModel model);
        Task<AuthDetailModel> GetPasswordByUsernameAsync(string username);
        Task<ResponseModel> SendOtpEmailAsync(string toEmail, string toName, string otpCode);
        Task<ResponseModel> SaveOtpAsync(OtpDetailModel model);
        Task<OtpDetailModel> GetOtpDetailAsync(string email);
        Task<ResponseModel> ClearOtpAsync(string email);
        Task<ResponseModel> IncrementOtpAttemptsAsync(Guid userId);
        Task<ResponseModel> ResetFailedAttemptsAsync(string email);
        Task<ResponseModel> ResetPasswordAsync(Guid userId, string passwordHash);
        Task<ResponseModel> SavePasswordResetTokenAsync(Guid userId, Guid token);
        Task<ResponseModel> ResetForgotPasswordAsync(ResetForgotPasswordModel model);
        Task<OtpDetailModel> GetOtpDetailByUserIdAsync(Guid userId);
        Task<ResponseModel> ClearForgotPasswordOtpAsync(Guid userId);
    }
}
