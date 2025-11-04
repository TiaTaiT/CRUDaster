using Auth0.AspNetCore.Authentication;
using CRUDaster.Components;
using CRUDaster.Core.Domain.Models;
using CRUDaster.ExternalServices;
using CRUDaster.ExternalServices.Services;
using CRUDaster.Infrastructure.Extensions;
using CRUDaster.Infrastructure.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using MudBlazor.Services;

// Instructs this specific application to prefer IPv4, fixing the timeout
// without affecting the rest of the server.
// It needs for Auth0
AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.PreferIPv4OverIPv6", true);

var builder = WebApplication.CreateBuilder(args);

// AUTH0 Configuration
builder.Services
    .AddAuth0WebAppAuthentication(options =>
    {
        options.Domain = builder.Configuration["Auth0:Domain"];
        options.ClientId = builder.Configuration["Auth0:ClientId"];
        options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
    })
    .WithAccessToken(options =>
    {
        options.Audience = builder.Configuration["Auth0:Audience"];
        options.UseRefreshTokens = true;
    });

// Enable Role claim mapping
builder.Services.AddScoped<IClaimsTransformation, RoleClaimsTransformation>();
builder.Services.Configure<CapsuleSettings>(
    builder.Configuration.GetSection("CapsuleConfig"));
// Authorization & Authentication
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<CookieForwardingHandler>();
// Add API `HttpClient` for same-origin cookie-auth (Default client)
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
})
.AddHttpMessageHandler<CookieForwardingHandler>();

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>()
      .CreateClient("ApiClient"));

// Services for your app
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddInfrastructureExternalServices(builder.Configuration);
builder.Services.AddControllers();

// Blazor & UI services
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.AddMudBlazorDialog();

var app = builder.Build();

// HTTP pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor |
                       Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapControllers();
app.MapStaticAssets();

// Auth0 login & logout endpoints
app.MapGet("/Account/Login", async (HttpContext httpContext, string returnUrl = "/") =>
{
    var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
        .WithRedirectUri(returnUrl)
        .Build();
    try
    {
        await httpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
});

app.MapGet("/Account/Logout", async (HttpContext httpContext) =>
{
    var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
        .WithRedirectUri("/")
        .Build();

    await httpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
});

// Blazor server rendering
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
