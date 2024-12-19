using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.InternalApi.Controllers;

[Route("api/internal/googleLogin")]
[ApiController]
public class GoogleLoginController : Controller
{
    [HttpPost("GoogleloginRequest")]
    public IActionResult GoogleLogin()
    {
        var redirectUrl = $"{Request.Scheme}://{Request.Host}{Url.Action("GoogleResponse", "GoogleLogin")}";
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }
    
    [HttpGet("soci")]
    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (result?.Principal == null)
        {
            return RedirectToAction("LoginFailed");
        }

        var claims = result.Principal.Identities.FirstOrDefault()?.Claims;
        var email = claims?.FirstOrDefault(c => c.Type == "emailaddress")?.Value;
        var name = claims?.FirstOrDefault(c => c.Type == "givenname")?.Value;
        var familyName = claims?.FirstOrDefault(c => c.Type == "surname")?.Value;
        var emailVerified = claims?.FirstOrDefault(c => c.Type == "email_verified")?.Value;
        var googleId = claims?.FirstOrDefault(c => c.Type == "sub")?.Value;

        HttpContext.Session.SetString("GoogleEmail", email);
        HttpContext.Session.SetString("GoogleName", name);
        HttpContext.Session.SetString("GoogleFamilyName", familyName);
        HttpContext.Session.SetString("GoogleEmailVerified", emailVerified);
        HttpContext.Session.SetString("GoogleId", googleId);

        // UserController’a yönlendirme
        return RedirectToAction("HandleGoogleLogin", "User");
    }
}