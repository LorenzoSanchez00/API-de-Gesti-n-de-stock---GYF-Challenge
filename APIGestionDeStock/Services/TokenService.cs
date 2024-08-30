using APIGestionDeStock.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIGestionDeStock.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration["Jwt:Key"]!));
        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
            };

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(signingCredentials: credentials, claims: claims, expires: DateTime.UtcNow.AddDays(5), audience: _configuration["Jwt:Audience"], issuer: _configuration["Jwt:Issuer"]);

            var tokenHandler = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return tokenHandler;
        }
    }
}
