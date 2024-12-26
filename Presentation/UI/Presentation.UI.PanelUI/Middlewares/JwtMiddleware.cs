using AutoMapper;
using Core.Application.Features.Queries.UserQueries.Queries;
using Core.Application.Helper;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;


namespace Presentation.UI.PanelUI.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;  
    private readonly string _secretKey;
    public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _secretKey = configuration["JwtBearer:ResetPasswordKey"]
                    ?? throw new Exception("ResetPasswordKey is not configured in appsettings.json");
    }

    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
    {
        
        using (var scope = serviceProvider.CreateScope())
        {
            var scopedMediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var jwtRepository = scope.ServiceProvider.GetRequiredService<IJwtRepository>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

            string token = context.Session.GetString("JwtToken");


            if (context.Request.Cookies.ContainsKey("RememberMe") && context.Request.Cookies.ContainsKey("XXXLogin"))
            {
                bool rememberMeValue = bool.Parse(context.Request.Cookies["RememberMe"]);

                if (rememberMeValue)
                {
                    var encryptUserId = context.Request.Cookies["XXXLogin"];
                    var decryptUserId = Cipher.DecryptUserId(encryptUserId, _secretKey);

                    var userRes = await scopedMediator.Send(new GetByIdUserQuery() { Id = Guid.Parse(decryptUserId) });

                    if (userRes is not null && token is null)
                    {
                        var user = mapper.Map<User>(userRes.Data);
                        token = jwtRepository.GenerateJwtToken(user);
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

}