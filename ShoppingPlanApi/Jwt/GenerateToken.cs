using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShoppingPlanApi.Jwt
{
    public class GenerateToken
    {
        private IConfiguration _configuration;
        public GenerateToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateTokenJwt(int userID, int roleID)
        {
            try
            {
                var mySecret = _configuration["JwtConfig:Secret"];
                var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

                var myIssuer = _configuration["JwtConfig:Issuer"];
                var myAudience = _configuration["JwtConfig:Audience"];
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
            new Claim("User", userID.ToString()),
            new Claim("Role",roleID.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = myIssuer,
                    Audience = myAudience,
                    SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
