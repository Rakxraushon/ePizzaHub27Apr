using ePizzaHub.Services;
using ePizzaHub.Services.Implementations;
using ePizzaHub.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using WebMarkupMin.AspNetCore7;

var builder = WebApplication.CreateBuilder(args);

//logging 
builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
ConfigureServices.RegisteredServices(builder.Services, builder.Configuration);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "ePizzahubapp";
        options.SlidingExpiration = true;
        options.LoginPath = "/account/login";
        options.AccessDeniedPath = "/account/unauthorize";
    });

builder.Services.AddWebMarkupMin(options =>
{
    options.AllowMinificationInDevelopmentEnvironment = true; options.AllowCompressionInDevelopmentEnvironment = true;
    options.DisablePoweredByHttpHeaders = true;
}).AddHtmlMinification(options =>
{
    options.MinificationSettings.RemoveRedundantAttributes = true; options.MinificationSettings.MinifyInlineJsCode = true; options.MinificationSettings.MinifyInlineCssCode = true; options.MinificationSettings.MinifyEmbeddedJsonData = true; options.MinificationSettings.MinifyEmbeddedCssCode = true;
}).AddHttpCompression();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        const int durationInSeconds = 60 * 60 * 24 * 7; //Secs*Mins*Hrs*Days
        ctx.Context.Response.Headers["cache-control"] = "public, max-age=" + durationInSeconds;
    }
});

app.UseRouting();
app.UseWebMarkupMin();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
         );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
