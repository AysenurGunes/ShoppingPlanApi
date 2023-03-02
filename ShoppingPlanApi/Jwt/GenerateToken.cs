using Microsoft.IdentityModel.Tokens;
using ShoppingPlanApi.Dtos;
using ShoppingPlanApi.Models;
using ShoppingPlanApi.Response;
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
        public async Task<string> GenerateTokenJwt(User user)
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
            new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
            new Claim(ClaimTypes.Role,user.Role.RoleName)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = myIssuer,
                    Audience = myAudience,
                    SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                //TokenResponse response = new TokenResponse
                //{
                //    AccessToken = token.ToString(),
                //    ExpireTime = DateTime.UtcNow.AddHours(1),
                //    Role = user.Role.RoleName,
                //    UserID = user.UserID.ToString(),
                //};
                // return new BaseResponse<TokenResponse>(response);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
