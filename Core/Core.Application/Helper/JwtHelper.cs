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
                
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = validIssuer,
                    ValidateAudience = true,
                    ValidAudience = validAudience,
                    ValidateLifetime = true, 
                    ClockSkew = TimeSpan.Zero 
                };
                
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

               
                var userId = principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    throw new SecurityTokenException("No user ID was found in the token.");
                }

                return userId;
            }
            catch (Exception ex)
            {               
                throw new SecurityTokenException($"Invalid token: {ex.Message}");
            }
        }

    }
}
