using Medicare.Application.Models.JwtTokens;
using System.Security.Claims;

namespace Medicare.Application.Interfaces.JwtToken
{
    public interface IJwtTokenRepository
    {
        string GenerateToken(JwtTokenClaimModel model);
        string GenerateRefreshToken();                           
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        ClaimsPrincipal ValidatePasswordResetToken(string token);
        string GeneratePasswordResetToken(string userId, string employeeId);
        string GenerateAppointmentConfirmationToken(int appointmentId);
        int? ValidateAppointmentConfirmationToken(string token);
    }
}
