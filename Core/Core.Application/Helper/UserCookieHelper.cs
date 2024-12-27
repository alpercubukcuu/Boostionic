using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Helper;

public static class UserCookieHelper
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

    public static void SetErrorMessage(IHttpContextAccessor httpContextAccessor, string message, int expirationMinutes = 5)
    {
        var request = httpContextAccessor?.HttpContext?.Request;
        var response = httpContextAccessor?.HttpContext?.Response;

        if (response == null)
        {
            throw new InvalidOperationException("HttpContext.Response is null. Unable to set error message cookie.");
        }
       
        if (request.Cookies.ContainsKey("ErrorMessage"))
        {
            response.Cookies.Delete("ErrorMessage");
        }

        response.Cookies.Append("ErrorMessage", message, new CookieOptions
        {
            HttpOnly = false, 
            SameSite = SameSiteMode.Lax,
            Expires = DateTime.Now.AddMinutes(expirationMinutes)
        });
    }



    public static string GetUserIdFromCookie(IHttpContextAccessor httpContextAccessor, string secretKey)
    {
        var request = httpContextAccessor?.HttpContext?.Request;

        if (request.Cookies.TryGetValue("XXXLogin", out var cookieValue) && !string.IsNullOrEmpty(cookieValue))
        {
            return Cipher.DecryptUserId(cookieValue, secretKey);
        }

        return string.Empty;
    }
}
