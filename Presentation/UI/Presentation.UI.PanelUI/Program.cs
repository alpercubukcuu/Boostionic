using Core.Application;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Presentation.UI.PanelUI.Middlewares;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices(builder.Configuration.GetConnectionString("Mssql")!);
builder.Services.AddApplicationServices();

var googleClientId = builder.Configuration["Authentication:Google:ClientId"];
var googleClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
var facebookClientId = builder.Configuration["Authentication:Facebook:AppId"];
var facebookClientSecret = builder.Configuration["Authentication:Facebook:AppSecret"];

builder.Services.AddHttpClient("InternalApiClient", client =>
{
    var baseUrl = builder.Configuration["ApiSettings:InternalApiBaseUrl"];
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    if (string.IsNullOrEmpty(baseUrl))
    {
        throw new Exception("Internal API base URL is not configured in appsettings.json");
    }

    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddHttpClient("ExternalApiClient", client =>
{
    var baseUrl = builder.Configuration["ApiSettings:ExternalApiBaseUrl"];
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    if (string.IsNullOrEmpty(baseUrl))
    {
        throw new Exception("External API base URL is not configured in appsettings.json");
    }

    client.BaseAddress = new Uri(baseUrl);
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews(config =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("AoGenLimit", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            partition => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 2
            }));
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;        
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = googleClientId;
        googleOptions.ClientSecret = googleClientSecret;
        googleOptions.Scope.Add("openid");
        googleOptions.Scope.Add("profile");
        googleOptions.Scope.Add("email");
        googleOptions.SaveTokens = true;
        
    })
    .AddFacebook(facebookOptions =>
    {
        facebookOptions.AppId = facebookClientId; 
        facebookOptions.AppSecret = facebookClientSecret;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["JwtBearer:ValidIssuer"], // Read directly from configuration
            ValidAudience = builder.Configuration["JwtBearer:ValidAudience"], // Read directly from configuration
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtBearer:IssuerSigningKey"]!)), // Secure key reading
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true // Ensures token expiration is checked
        };

        // Configure error handling for unauthorized responses
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                var result = System.Text.Json.JsonSerializer.Serialize(new { error = "Unauthorized" });
                return context.Response.WriteAsync(result);
            },
            OnForbidden = context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";
                var result = System.Text.Json.JsonSerializer.Serialize(new { error = "Forbidden" });
                return context.Response.WriteAsync(result);
            }
        };
    });


builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseSession();
app.UseMiddleware<JwtMiddleware>();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=RegisterPage}/{id?}");

app.Run();