using CleanDinner.Application.Common.Authentication;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace CleanDinner.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private IConfiguration _config;
        private JwtSettings _jwtSettings;

        public JwtTokenGenerator(IConfiguration config, IOptions<JwtSettings> jwtSettings)
        {
            _config = config;
            _jwtSettings = jwtSettings.Value;// Options pattern we will be discussing later
        }

        public string GenerateToken(Guid userId, string firstName, string lastName)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);
            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName,firstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}

    

