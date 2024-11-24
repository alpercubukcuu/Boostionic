using AutoMapper;
using Core.Application.Features.Queries.UserQueries.Queries;
using Core.Application.Helper;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;


namespace Presentation.UI.PanelUI.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IJwtRepository _jwtRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public JwtMiddleware(RequestDelegate next, IMediator mediator, IMapper mapper)
    {
        _next = next;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
    {
        string token = "";
        token = context.Session.GetString("JwtToken");

        if (context.Request.Cookies.ContainsKey("RememberMe") && context.Request.Cookies.ContainsKey("XXXLogin"))
        {
            bool rememberMeValue = bool.Parse(context.Request.Cookies["RememberMe"]);

            if (rememberMeValue)
            {
                var encryptUserId = context.Request.Cookies["XXXLogin"];
                var decryptUserId = Cipher.DecryptUserId(encryptUserId);

                var userRes = _mediator.Send(new GetByIdUserQuery() { Id = Guid.Parse(decryptUserId) });
                  

                if (userRes is not null && token is null)
                {
                    var map = _mapper.Map<User>(userRes);
                    token = _jwtRepository.GenerateJwtToken(map);
                }

                context.Session.SetString("JwtToken", token);
                
                if (context.Request.Path == "/" || context.Request.Path == "/User/UserLogin")
                {
                    context.Request.Headers["Authorization"] = "Bearer " + token;
                    context.Response.Redirect("/Home/Index");
                    return;
                }
            }
          
        }

        if (!string.IsNullOrEmpty(token))
        {
            context.Request.Headers["Authorization"] = "Bearer " + token;
        }


        await _next(context);
    }
}