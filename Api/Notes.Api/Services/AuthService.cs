﻿using System.IdentityModel.Tokens.Jwt;
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
            var key = Encoding.ASCII.GetBytes(AuthSettings.PrivateKey);
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = credentials
            };
            var token = handler.CreateToken(tokenDescriptor);
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
            var email = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            if (email is null)
            {
                return null;
            }
            return email.Value;
        }
    }
}
