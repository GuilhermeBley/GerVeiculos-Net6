using ClienteNet6.Shared.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;

namespace ClienteNet6.Server.Services
{
    /// <summary>
    /// User service
    /// </summary>
    public interface IUserService
    {
        UserInfo GetUser();
    }

    /// <summary>
    /// Gets currently user information
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _contextAcessor;
        private static readonly UserInfo EmptyUser = new UserInfo();

        public UserService(IHttpContextAccessor contextAccessor)
        {
            _contextAcessor = contextAccessor;
        }

        public UserInfo GetUser()
        {
            string token = ((string)_contextAcessor.HttpContext.Request.Headers.Authorization).Replace(JwtBearerDefaults.AuthenticationScheme,"");
            
            if (string.IsNullOrEmpty(token))
                return EmptyUser;

            var tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(token))
            {
                return EmptyUser;
            }

            var claims = tokenHandler.ReadJwtToken(token).Claims;

            var nome = claims.FirstOrDefault(predicate => predicate.Type == JwtRegisteredClaimNames.Name);
            var email = claims.FirstOrDefault(predicate => predicate.Type == JwtRegisteredClaimNames.Email);
            var empresa = claims.FirstOrDefault(predicate => predicate.Type == JwtRegisteredClaimNames.FamilyName);
            var identificador = claims.FirstOrDefault(predicate => predicate.Type == JwtRegisteredClaimNames.NameId);

            return new UserInfo
            {
                Email = email is null ? "" : email.Value,
                Empresa = empresa is null ? "" : empresa.Value,
                Id = identificador is null ? 0 : int.Parse(identificador.Value),
                UserName = nome is null ? "" : nome.Value
            };

        }
    }
}
