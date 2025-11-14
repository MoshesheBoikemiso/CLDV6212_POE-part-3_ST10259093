using ABCRetailers.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAzureStorageService, AzureStorageService>();


builder.Services.AddHttpClient<FunctionsService>();
builder.Services.AddScoped<FunctionsService>();
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddScoped<SqlAuthService>();
builder.Services.AddScoped<OrderService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<SqlAuthService>();

builder.Services.AddScoped<ShoppingCartService>();

builder.Services.AddScoped<SqlProductService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customer}/{action=Index}/{id?}");


app.Run();