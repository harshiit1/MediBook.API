using Medicare.Application.Interfaces.JwtToken;
using Medicare.Application.Models.JwtTokens;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Medicare.DAL.Services
{
    public class JwtService : IJwtTokenRepository
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(JwtTokenClaimModel model)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SigningKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claim = new[]
           {
                new Claim(JwtRegisteredClaimNames.Sub,          model.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,        model.Email ?? ""),
                new Claim(JwtRegisteredClaimNames.UniqueName,   model.Username ?? "" ),
                new Claim(JwtRegisteredClaimNames.Jti,          Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,          DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.Role,                      model.RoleName),
                new Claim("UserId",                             model.UserId.ToString()),
                new Claim("RefId",                              model.RefId.ToString()),
                new Claim("UserType",                           model.UserType),
                new Claim("FullName",                           model.FullName ?? ""),
                new Claim("TenantId",                           model.TenantId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claim,
                signingCredentials: creds,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JwtSettings:TokenExpiryMinutes"]))
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GeneratePasswordResetToken(string userId, string employeeId)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JwtSettings:SigningKey"]));

            var claims = new[]
            {
                new Claim("userId",     userId),
                new Claim("employeeId", employeeId),
                new Claim("purpose",    "password-reset"), // prevents reuse of auth tokens
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30), // short lived
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateRefreshToken()
        {
            var bytes = new byte[64];
            using var bytesGenerator = RandomNumberGenerator.Create();
            bytesGenerator.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SigningKey"]));

            var validation = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = _configuration["JwtSettings:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["JwtSettings:Audience"],
                ValidateLifetime = false
            };

            var principal = new JwtSecurityTokenHandler().ValidateToken(token, validation, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwt || !jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
        public ClaimsPrincipal ValidatePasswordResetToken(string token)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JwtSettings:SigningKey"]));

            var validation = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = _configuration["JwtSettings:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["JwtSettings:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var principal = new JwtSecurityTokenHandler()
                .ValidateToken(token, validation, out SecurityToken securityToken);

            var purpose = principal.FindFirstValue("purpose");
            if (purpose != "password-reset")
                throw new SecurityTokenException("Invalid token purpose.");

            return principal;
        }
        public string GenerateAppointmentConfirmationToken(int appointmentId)
        {
            var key = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_configuration["JwtSettings:SigningKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim("AppointmentId", appointmentId.ToString()),
            new Claim("Purpose",       "AppointmentConfirmation"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public int? ValidateAppointmentConfirmationToken(string token)
        {
            var key = new SymmetricSecurityKey(
                          Encoding.UTF8.GetBytes(_configuration["JwtSettings:SigningKey"]));

            var parameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = _configuration["JwtSettings:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["JwtSettings:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var principal = handler.ValidateToken(token, parameters, out _);

                var purpose = principal.FindFirstValue("Purpose");
                if (purpose != "AppointmentConfirmation")
                    return null;

                return int.Parse(principal.FindFirstValue("AppointmentId")!);
            }
            catch
            {
                return null;
            }
        }
    }
}
