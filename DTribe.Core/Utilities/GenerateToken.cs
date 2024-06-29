using DTribe.Core.DTO;
//using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core.Utilities
{
    public static class GenerateToken
    {
        public static async Task<string> GenerateJwtToken(UserInfoDTO user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_very_secure_random_secret_key_1234567890"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
               new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
               new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
               // Add other claims as needed
            };

            var token = new JwtSecurityToken(
                issuer: "your_issuer",
                audience: "your_audience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
