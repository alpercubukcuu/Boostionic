using Microsoft.AspNetCore.Http;

namespace Core.Application.Helper;

public static class UserRememberCookieHelper
{
    
    public static void HandleXXXLoginCookie(string userId, CookieOptions options, IHttpContextAccessor httpContextAccessor, string secretKey)
    {
        const string cookieName = "XXXLogin";

        var request = httpContextAccessor?.HttpContext?.Request;
        var response = httpContextAccessor?.HttpContext?.Response;

        if (request == null || response == null)
        {
            throw new InvalidOperationException("HttpContext is not available.");
        }

        if (!request.Cookies.ContainsKey(cookieName))
        {
            // Yeni Cookie Oluştur
            var xxxLoginValue = Cipher.EncryptUserId(userId, secretKey);
            response.Cookies.Append(cookieName, xxxLoginValue.ToString(), options);
        }
        else
        {
            // Mevcut Cookie'yi Kontrol Et
            var cookieValue = request.Cookies[cookieName];
            var parts = cookieValue.Split('|');

            if (parts.Length == 2 && DateTime.TryParse(parts[1], out var expirationDate))
            {
                if (expirationDate < DateTime.Now)
                {
                    // Süresi Dolmuşsa Yeniden Oluştur
                    ReplaceXXXLoginCookie(userId, options, httpContextAccessor, secretKey);
                }
            }
            else
            {
                // Geçersiz Cookie, Yeniden Oluştur
                ReplaceXXXLoginCookie(userId, options, httpContextAccessor, secretKey);
            }
        }
    }

    public static void ReplaceXXXLoginCookie(string userId, CookieOptions cookieOptions, IHttpContextAccessor httpContextAccessor, string secretKey)
    {
        const string cookieName = "XXXLogin";

        var response = httpContextAccessor?.HttpContext?.Response;

        if (response == null)
        {
            throw new InvalidOperationException("HttpContext is not available.");
        }

        response.Cookies.Delete(cookieName);

        var newExpiration = DateTime.Now.AddMinutes(30);
        var newXxxLoginValue = $"{Cipher.EncryptUserId(userId, secretKey)}|{newExpiration:O}";
        response.Cookies.Append(cookieName, newXxxLoginValue, cookieOptions);
    }
}
