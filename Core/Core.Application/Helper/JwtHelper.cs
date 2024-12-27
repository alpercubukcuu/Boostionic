using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Helper
{
    public static class JwtHelper
    {

        public static string GetUserIdFromToken(string token, IConfiguration _configuration)
        {

            var signingKey = _configuration["JwtBearer:IssuerSigningKey"];
            var validIssuer = _configuration["JwtBearer:ValidIssuer"];
            var validAudience = _configuration["JwtBearer:ValidAudience"];

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(signingKey);

                // Token doğrulama kuralları
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = validIssuer,
                    ValidateAudience = true,
                    ValidAudience = validAudience,
                    ValidateLifetime = true, // Token süresi dolmuşsa hata verir
                    ClockSkew = TimeSpan.Zero // Token doğrulaması sırasında saat farkı tanımlanmaz
                };

                // Token doğrulama ve çözümleme
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                // Kullanıcı kimliği claim'den alınır
                var userId = principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    throw new SecurityTokenException("No user ID was found in the token.");
                }

                return userId;
            }
            catch (Exception ex)
            {
                // Hata fırlatma yerine loglama eklenebilir
                throw new SecurityTokenException($"Invalid token: {ex.Message}");
            }
        }

    }
}
