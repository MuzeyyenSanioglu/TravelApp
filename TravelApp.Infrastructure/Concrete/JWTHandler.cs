using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelApp.Domain.Entities;
using TravelApp.Infrastructure.Concrete.Interfaces;

namespace TravelApp.Infrastructure.Concrete
{
    public class JWTHandler : ITokenHandler
    {
        private IConfiguration Configuration { get; }  // appsetting dosyasından okumak için
        private TokenOptions TokenOptions { get; }
        private readonly DateTime _accessTokenExpiration;
        public JWTHandler(IConfiguration configuration)
        {
            Configuration = configuration;
            TokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            _accessTokenExpiration = DateTime.Now.AddMinutes(TokenOptions.AccessTokenExpiration);
        }

        public AccessToken CreateToken(TokenRequestModel user, List<OperationClaim> operationClaims = null)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenOptions.SecurityKey));
            var signingCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var jwt = new JwtSecurityToken(
                issuer: TokenOptions.Issuer,
                audience: TokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredential
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new AccessToken()
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }
        private IEnumerable<Claim> SetClaims(TokenRequestModel user, List<OperationClaim> operationClaims = null)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("username", user.username));
            if (operationClaims != null)
                operationClaims.Select(x => x.Name).ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
            return claims;
        }

    }
}
