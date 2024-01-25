using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DragonAPIH3.Interfaces;

namespace DragonAPIH3.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _issuer = "http://localhost:5071/"; // Replace with your issuer
        private readonly string _audience = "http://localhost:5071/"; // Replace with your audience

        private readonly byte[] _secretKey;

        public JwtService(IConfiguration configuration)
        {
            // Fetch the secret key from your configuration or use a secure method to store/retrieve it
            _secretKey = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
        }
        public string GenerateToken(string username)
        {
            var key = new SymmetricSecurityKey(_secretKey);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateToken(string token, out ClaimsPrincipal principal)
        {
            principal = null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = _secretKey;

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out SecurityToken validatedToken);

                principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
