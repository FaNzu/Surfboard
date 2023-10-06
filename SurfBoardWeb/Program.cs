using SurfBoardWeb.Data.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SurfBoardWeb.Data;
using Microsoft.AspNetCore.Identity;
using SurfBoardWeb.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SurfBoardWebContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("SurfBoardWebContext") ?? throw new InvalidOperationException("Connection string 'SurfBoardWebContext' not found.")));

builder.Services.AddDefaultIdentity<DefaultUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SurfBoardWebContext>();
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
    var services = scope.ServiceProvider;

   SeedData.Initialize(services);
}

using (var scope = app.Services.CreateScope())
{
    var roleM = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleM.RoleExistsAsync(role))
        {
            await roleM.CreateAsync(new IdentityRole(role));
        }
    }
}

using (var scope = app.Services.CreateScope())
{
    var userM = scope.ServiceProvider.GetRequiredService<UserManager<DefaultUser>>();

    string email = "admin@admin.com";
    string password = "Admin4$";
    if (await userM.FindByEmailAsync(email) == null)
    {
        var user = new DefaultUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        var result = await userM.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await userM.AddToRoleAsync(user, "Admin");
        }
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
    app.UseAuthentication();;

app.UseAuthorization();

			

app.MapControllerRoute(
	name: "default", 
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.UseAuthorization();

app.UseRequestLocalization("da-FR");

app.Run();

