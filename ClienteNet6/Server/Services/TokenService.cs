using ClienteNet6.Server.Identity;
using ClienteNet6.Shared.Dto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClienteNet6.Server.Services
{
    public interface ITokenService
    { 
        UserTokenJwt GetToken(User user);
    }

    public class TokenService : ITokenService
    {
        public IConfiguration _Configuration { get; }

        public TokenService(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public UserTokenJwt GetToken(User usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_Configuration["Jwt:Key"]);

            // Gerando token
            var expires = DateTime.UtcNow.AddHours(8);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

                // Claims
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuario.UserName),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Email)
                })
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
             
            return new UserTokenJwt()
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = expires
            };
        }
    }
}
