using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DragonAPIH3.Services
{
    public class JwtService
    {
        private readonly string _issuer; // Replace with your issuer
        private readonly string _audience; // Replace with your audience

        private readonly byte[] _secretKey;

        public JwtService(IConfiguration configuration)
        {
            // Fetch the secret key from your configuration or use a secure method to store/retrieve it
            _secretKey = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            _audience = configuration["Jwt:Audience"];
            _issuer = configuration["Jwt:Issuer"];
        }
        public string GenerateToken(string username)
        {
            var key = new SymmetricSecurityKey(_secretKey);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[] 
{
                //new Claim(ClaimTypes.Name, username),
};

            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                null,
                DateTime.Now.AddMinutes(5),
                credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
