using GreenYellowSite.Models;
using GreenYellowSite.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddTransient<IEmailService, SmtpEmailService>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (!app.Environment.IsDevelopment())
{
    app.Use(async (context, next) =>
    {
        context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
        context.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";
        context.Response.Headers["X-Content-Type-Options"] = "nosniff";
        context.Response.Headers["Content-Security-Policy"] =
     "default-src 'self'; " +
     "img-src 'self' data: https://www.google-analytics.com https://www.googletagmanager.com; " +
     "style-src 'self' 'unsafe-inline'; " +
     "script-src 'self' 'unsafe-inline' https://www.googletagmanager.com; " +
     "font-src 'self' data:; " +
     "connect-src 'self' ws: wss: https://www.google-analytics.com https://region1.google-analytics.com https://www.googletagmanager.com;";
        await next();
    });
}
else
{
    app.Use(async (context, next) =>
    {
        context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
        context.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";
        context.Response.Headers["X-Content-Type-Options"] = "nosniff";
        await next();
    });
}

// This works only after the host has bound www.prook.lv and installed its TLS certificate.
app.Use(async (context, next) =>
{
    if (string.Equals(context.Request.Host.Host, "www.prook.lv", StringComparison.OrdinalIgnoreCase))
    {
        var target = "https://prook.lv" + context.Request.PathBase.ToUriComponent()
            + context.Request.Path.ToUriComponent() + context.Request.QueryString.ToUriComponent();
        context.Response.Redirect(target, permanent: true, preserveMethod: true);
        return;
    }
    await next();
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();