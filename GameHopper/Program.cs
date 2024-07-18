using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GameHopper;
using System.Configuration;
using GameHopper.Models;
using Microsoft.AspNetCore.Http.Features;
using GameHopper.Services;


var builder = WebApplication.CreateBuilder(args);

var connectionString = "server=localhost;user=crazyfrog;password=crazyfrog;database=gamehopper";
var serverVersion = new MySqlServerVersion(new Version(8,0,38));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100 MB
});

builder.Services.AddDbContext<GameDbContext>(dbContextOptions => dbContextOptions.UseMySql(connectionString, serverVersion));


builder.Services.AddDefaultIdentity<User>
(options =>
{
options.SignIn.RequireConfirmedAccount = false;
options.Password.RequireDigit = false;
options.Password.RequiredLength = 10;
options.Password.RequireNonAlphanumeric = false;
options.Password.RequireUppercase = true;
options.Password.RequireLowercase = false;
}).AddRoles<IdentityRole>().AddEntityFrameworkStores<GameDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<SignInManager<User>>();

builder.Services.AddScoped<EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    // Seed roles
    string[] roleNames = { "Admin", "GameMaster", "Player" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Seed admin user
    var adminEmail = "admin@admin.com";
    var adminPassword = "TestAdmin123";
    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var adminUser = new Player(adminEmail)
        {
            UserName = adminEmail,
            Email = adminEmail
        };
        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

app.Run();

// using (var scope = app.Services.CreateScope())
// {
//     var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

//     var roles = new[] {"Admin", "GameMaster", "Player"};

//     foreach (var role in roles)
//     {
//         if(!await roleManager.RoleExistsAsync(role));
//         await roleManager.CreateAsync(new IdentityRole(role));
//     }

//     var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
//     string adminEmail = "admin@admin.com";
//     string adminPassword = "Test@Admin123";
//     if (await userManager.FindByEmailAsync(adminEmail) == null)
//     {
//         var adminUser = new Player { UserName = adminEmail, Email = adminEmail };
//         var createUserResult = await userManager.CreateAsync(adminUser, adminPassword);
//         if (createUserResult.Succeeded)
//         {
//             await userManager.AddToRoleAsync(adminUser, "Admin");
//         }
//     }
// }