
using Medicare.Application.Models.CommonModels.ResponseModel;
using Medicare.Application.Models.JwtTokens;

namespace Medicare.Application.Interfaces.JwtToken
{
    public interface IRefreshTokenRepository
    {
        Task<ResponseModel> SaveRefreshTokenAsync(JwtRefreshTokenModel model);
        Task<RefreshTokenDto> ValidateRefreshTokenAsync(string token);
        Task<ResponseModel> RevokeRefreshTokenAsync(string token, string replacedBy = null);
    }
}
