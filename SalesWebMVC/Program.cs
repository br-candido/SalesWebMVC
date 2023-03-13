using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMVC.Data;
var builder = WebApplication.CreateBuilder(args);
var serverVersion = new MySqlServerVersion(new Version(8, 0, 32));
builder.Services.AddDbContext<SalesWebMVCContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("SalesWebMVCContext") ?? throw new InvalidOperationException("Connection string 'SalesWebMVCContext' not found."), serverVersion));

builder.Services.AddScoped<SeedingService, SeedingService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var sampleService = scope.ServiceProvider.GetRequiredService<SeedingService>();
    sampleService.Seed();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
