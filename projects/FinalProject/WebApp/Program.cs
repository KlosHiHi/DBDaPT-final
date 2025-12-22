using ShoeShopLibrary.Contexts;
using ShoeShopLibrary.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ShoeShopDbContext>();
builder.Services.AddScoped<AuthService>();

CultureInfo.DefaultThreadCurrentCulture =
    new("ru-RU") { NumberFormat = { NumberDecimalSeparator = "." } };

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
