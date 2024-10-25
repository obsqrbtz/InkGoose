using System.IdentityModel.Tokens.Jwt;
using System.Text;
using InkGoose.Domain.Entities;
using InkGoose.Api.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace InkGoose.Api.Services
{
    public class AuthService
    {
        public string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(AuthSettings.PrivateKey);
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = credentials
            };
            SecurityToken? token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
        private static ClaimsIdentity GenerateClaims(User user)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            return claims;
        }
        public static string? GetEmail(ClaimsPrincipal user)
        {
            Claim? email = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            return email switch
            {
                null => null,
                _ => email.Value
            };
        }
    }
}
