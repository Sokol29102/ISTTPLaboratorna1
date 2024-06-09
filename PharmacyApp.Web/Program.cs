using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PharmacyApp.Domain.Entities;
using PharmacyApp.Services;
using System;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Identity services
builder.Services.AddIdentity<User, IdentityRole>()
    .AddUserStore<UserStore>()
    .AddRoleStore<RoleStore>()
    .AddDefaultTokenProviders();

// Configure file-based storage services
var dataStoragePath = Path.Combine(AppContext.BaseDirectory, "data");
if (!Directory.Exists(dataStoragePath))
{
    Directory.CreateDirectory(dataStoragePath);
}
builder.Services.AddSingleton<IDataStorage<User>>(new FileDataStorage<User>(Path.Combine(dataStoragePath, "users.json")));
builder.Services.AddSingleton<IDataStorage<IdentityRole>>(new FileDataStorage<IdentityRole>(Path.Combine(dataStoragePath, "roles.json")));
builder.Services.AddSingleton<IDataStorage<Medicine>>(new FileDataStorage<Medicine>(Path.Combine(dataStoragePath, "medicines.json")));
builder.Services.AddSingleton<IDataStorage<Disease>>(new FileDataStorage<Disease>(Path.Combine(dataStoragePath, "diseases.json")));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
