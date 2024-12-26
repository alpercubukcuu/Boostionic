using Core.Application.Dtos;
using Core.Application.Features.Queries.UserQueries.Queries;
using Core.Application.Helper;
using Core.Application.Interfaces.Dtos;
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

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IMediator mediator)
        {
            _logger = logger;
            _secretKey = configuration["JwtBearer:ResetPasswordKey"]
                     ?? throw new Exception("ResetPasswordKey is not configured in appsettings.json");
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            string cookieValue = Request.Cookies["XXXLogin"];
            var userId = Cipher.DecryptUserId(cookieValue, _secretKey);

            IResultDataDto<UserDto> userData = await this._mediator.Send(new GetByIdUserQuery() { Id = Guid.Parse(userId) });
            if (!userData.IsSuccess) { return RedirectToAction("ErrorPage", "Error", userData.Error); }

            return View(userData.Data);
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
