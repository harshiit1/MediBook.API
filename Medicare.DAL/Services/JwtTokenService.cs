using Medicare.Application.Interfaces.IToken;
using Medicare.Application.Models.Associate;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Medicare.DAL.Services
{
    public class JwtTokenService : IJwtTokenInterface
    {
        private readonly IConfiguration _configuration;
        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(AssociateDetailModel model)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, model.AssociateId.ToString()),
                new Claim(ClaimTypes.Name, model.EmployeeId),
                new Claim(ClaimTypes.Email, model.EmailId),
                new Claim(ClaimTypes.Role, model.RoleId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(double.Parse(_configuration["JwtSettings:ExpiryInHours"])),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
