using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurfProxyApi.Data;

namespace SurfProxyApi.Seeders
{
    public class SeedAccounts
    {
        //public static async void Initialize(IServiceProvider serviceProvider)
        //{
        //    using (var context = new SurfBoardWebContext(serviceProvider.GetRequiredService<DbContextOptions<SurfBoardWebContext>>()))
        //    {
        //        var userManager = new RoleStore<IdentityRole>(context);

        //        if (userManager.Roles.Any())
        //        {
        //            Console.WriteLine("[~] Accounts have already been Seeded, continuing ...");
        //            return;
        //        }
        //        await userManager.CreateAsync()
        //    }
        //}
    }
}
