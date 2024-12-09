using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


namespace Core.Application.Helper
{
    using BCrypt.Net;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class Cipher
    {
        public static string Encrypt(string password)
        {
            return BCrypt.HashPassword(password);
        }

        public static bool Decrypt(string inputPassword, string password)
        {
            return BCrypt.Verify(inputPassword, password);
        }


        public static string EncryptUserId(string userId, string securityKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(securityKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UserId", userId) }),
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string DecryptUserId(string token, string securityKey)
        {

            string jwt = token.Split('|')[0];

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(securityKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var principal = tokenHandler.ValidateToken(jwt, validationParameters, out var validatedToken);
                return principal.FindFirst("UserId")?.Value;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

       

    }
}
