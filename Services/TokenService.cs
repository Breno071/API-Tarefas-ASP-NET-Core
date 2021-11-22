using GerenciamentoDeTarefas_ASP_NET_Core.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using GerenciamentoDeTarefas_ASP_NET_Core.Confi;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDeTarefas_ASP_NET_Core.Services
{
    public static class TokenService
    {
        public static string TokenGenerator(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Config.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.login.ToString()),
                    new Claim(ClaimTypes.Role, usuario.nivel_Autorizacao.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
