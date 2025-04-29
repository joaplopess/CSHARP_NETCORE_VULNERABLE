var builder = WebApplication.CreateBuilder(args);

// Security misconfigurations
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new Microsoft.AspNetCore.Mvc.IgnoreAntiforgeryTokenAttribute());
});

// Register DatabaseContext as a scoped service
builder.Services.AddScoped<DatabaseContext>();

var app = builder.Build();

// Initialize database on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    // This will ensure tables are created
}

// More misconfigurations
app.Use(async (context, next) =>
{
    context.Response.Headers.Remove("X-Frame-Options");
    context.Response.Headers.Remove("X-Content-Type-Options");
    context.Response.Headers.Remove("Content-Security-Policy");
    await next();
});

app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = true,
    DefaultContentType = "application/octet-stream"
});

app.UseRouting();

// Disabled security headers
app.Use(async (context, next) =>
{
    context.Response.Headers["Server"] = "VulnerableServer/1.0";
    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();