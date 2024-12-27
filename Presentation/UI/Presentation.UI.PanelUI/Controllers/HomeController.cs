using Core.Application.Features.Queries.UserQueries.Queries;
using Core.Application.Helper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.UI.PanelUI.Models;
using System.Diagnostics;

namespace Presentation.UI.PanelUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _secretKey;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _secretKey = configuration["JwtBearer:ResetPasswordKey"]
                         ?? throw new Exception("ResetPasswordKey is not configured in appsettings.json");
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                string userId = string.Empty;
               
                string cookieValue = Request.Cookies["XXXLogin"];
                if (!string.IsNullOrEmpty(cookieValue))
                {
                    userId = Cipher.DecryptUserId(cookieValue, _secretKey);
                }
                else
                {                   
                    var jwtToken = _httpContextAccessor.HttpContext?.Session.GetString("JwtToken");

                    if (string.IsNullOrEmpty(jwtToken))
                    {
                        return RedirectToAction("RegisterPage", "User");
                    }

                    userId = JwtHelper.GetUserIdFromToken(jwtToken, _configuration);
                }
              
                if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsedUserId))
                {
                    return RedirectToAction("ErrorPage", "Error", new { errorMessage = "User ID is invalid or missing." });
                }
               
                var userData = await _mediator.Send(new GetByIdUserQuery { Id = parsedUserId });

                if (!userData.IsSuccess)
                {
                    return RedirectToAction("ErrorPage", "Error", new { errorMessage = userData.Error });
                }
                
                return View(userData.Data);
            }
            catch (Exception ex)
            {                
                return RedirectToAction("ErrorPage", "Error", new { errorMessage = "An unexpected error occurred." });
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
