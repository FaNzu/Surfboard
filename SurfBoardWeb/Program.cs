using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SurfBoardWeb.Data;
using Microsoft.AspNetCore.Identity;
using SurfBoardWeb.Models;
using System;

namespace SurfBoardWeb
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddDbContext<SurfBoardWebContext>(options =>
			    options.UseSqlServer(builder.Configuration.GetConnectionString("SurfBoardWebContext") ?? throw new InvalidOperationException("Connection string 'SurfBoardWebContext' not found.")));

               builder.Services.AddDefaultIdentity<DefaultUser>(options => options.SignIn.RequireConfirmedAccount = true)
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
		}
		
	}
}