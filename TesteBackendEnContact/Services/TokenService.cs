using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TesteBackendEnContact.Core.Domain.User;
using TesteBackendEnContact.Services.Interface;
namespace TesteBackendEnContact.Services
{
    public class TokenService : ITokenService
    {
        public string CreateToken(User user)
        {
            // Primeiro se cria um token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            // Tranforma a chave em byte array pra ser usado posteriormente
            var key = Encoding.ASCII.GetBytes(KeyConfiguration.Key);

            //Token descriptor possui todas as configurações do token, como duração, claims, como ele vai fazer a desencriptação
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email)
                })
            };

            // cria-se o token 
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // transforma elem em string para retornar
            return tokenHandler.WriteToken(token);

        }
    }
}
