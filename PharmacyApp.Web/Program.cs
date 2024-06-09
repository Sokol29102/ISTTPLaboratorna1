using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PharmacyApp.Domain.Entities;
using System;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<User, IdentityRole>()
    .AddUserStore<UserStore>()
    .AddRoleStore<RoleStore>()
    .AddDefaultTokenProviders();

var dataStoragePath = Path.Combine(AppContext.BaseDirectory, "data");
if (!Directory.Exists(dataStoragePath))
{
    Directory.CreateDirectory(dataStoragePath);
}
builder.Services.AddSingleton<IDataStorage<User>>(new FileDataStorage<User>(Path.Combine(dataStoragePath, "users.json")));
builder.Services.AddSingleton<IDataStorage<IdentityRole>>(new FileDataStorage<IdentityRole>(Path.Combine(dataStoragePath, "roles.json")));
builder.Services.AddSingleton<IDataStorage<Medicine>>(new FileDataStorage<Medicine>(Path.Combine(dataStoragePath, "medicines.json")));
builder.Services.AddSingleton<IDataStorage<Disease>>(new FileDataStorage<Disease>(Path.Combine(dataStoragePath, "diseases.json")));

builder.Services.AddAuthentication();
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "login",
    pattern: "{controller=Account}/{action=Login}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedData.Initialize(userManager, roleManager);
}

app.Run();
