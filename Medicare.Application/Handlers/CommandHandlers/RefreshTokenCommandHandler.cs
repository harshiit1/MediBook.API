using MediatR;
using Medicare.Application.Features.Commands.Authentication;
using Medicare.Application.Interfaces.JwtToken;
using Medicare.Application.Models.JwtTokens;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Medicare.Application.Handlers.CommandHandlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponseModel>
    {
        private readonly IJwtTokenRepository _jwtTokenRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IConfiguration _configuration;
        public RefreshTokenCommandHandler(IJwtTokenRepository jwtTokenRepository,  IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtTokenRepository = jwtTokenRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _configuration = configuration;
        }
        public async Task<RefreshTokenResponseModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            ClaimsPrincipal principal = _jwtTokenRepository.GetPrincipalFromExpiredToken(request.model.AccessToken);

            RefreshTokenDto validateToken = await _refreshTokenRepository.ValidateRefreshTokenAsync(request.model.RefreshToken);

            Guid userIdClaim = Guid.Parse(principal.FindFirst("UserId")!.Value);
            
            if (validateToken.UserId != userIdClaim)
            {
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }

            string accessToken = _jwtTokenRepository.   GenerateToken(new JwtTokenClaimModel
            {
                UserId = userIdClaim,
                RefId = int.Parse(principal.FindFirst("RefId")?.Value ?? "0"),
                UserType = principal.FindFirst("UserType")?.Value ?? "",
                Email = principal.FindFirst(ClaimTypes.Email)?.Value ?? "",
                Username = principal.FindFirst(ClaimTypes.Name)?.Value ?? "",
                FullName = principal.FindFirst("FullName")?.Value ?? "",
                RoleName = principal.FindFirst(ClaimTypes.Role)?.Value ?? "",
                TenantId = Guid.TryParse(principal.FindFirst("TenantId")?.Value, out Guid tid)
                           ? tid : Guid.Empty
            });

            string generatedRefreshToken = _jwtTokenRepository.GenerateRefreshToken();
            int expDays = int.Parse(_configuration["JwtSettings:RefreshTokenExpDays"]);
            DateTime expiresDate = DateTime.UtcNow.AddDays(expDays);

            await _refreshTokenRepository.RevokeRefreshTokenAsync(request.model.RefreshToken, generatedRefreshToken);

            var refreshTokenData = new JwtRefreshTokenModel
            {
                UserId = userIdClaim,
                UserType = validateToken.UserType,
                RefreshToken = generatedRefreshToken,
                ExpiryDate = expiresDate
            };
            var result = await _refreshTokenRepository.SaveRefreshTokenAsync(refreshTokenData);

            return new RefreshTokenResponseModel
            {
                AccessToken = accessToken,
                RefreshToken = generatedRefreshToken,
                ExpiryDate = DateTime.UtcNow.AddMinutes(
                    double.Parse(_configuration["JwtSettings:TokenExpiryMinutes"])),
                IsSuccess = result.IsSuccess,
                ResponseMessage = result.ResponseMessage
            };
        }
    }
}
