using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using SurfBoardWeb.Models;

namespace SurfBoardWeb.Data.SeedData
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SurfBoardWebContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SurfBoardWebContext>>()))
            {
                if (context.Board.Any())
                {
                    Console.WriteLine("[~] Surfboards have already been Seeded, continuing ...");
                    return;
                }

                context.Board.AddRange(
                    new Board
                    {
                        Name = "The Minilog",
                        Length = 6,
                        Width = 21,
                        Thickness = 2.75,
                        Volume = 38.8,
                        Type = "Shortboard",
                        Price = 565,
                        Equipment = null,
                        PicturePath = null
                    },
                    new Board
                    {
                        Name = "The Wide Glider",
                        Length = 7.1,
                        Width = 21.75,
                        Thickness = 2.75,
                        Volume = 44.16,
                        Type = "Funboard",
                        Price = 685,
                        Equipment = null,
                        PicturePath = "https://cdn2.editmysite.com/images/blank.gif"
                    },
                    new Board
                    {
                        Name = "The Golden Ratio",
                        Length = 6.3,
                        Width = 21.85,
                        Thickness = 2.75,
                        Volume = 44.22,
                        Type = "Funboard",
                        Price = 695,
                        Equipment = null,
                        PicturePath = null
                    },
                    new Board
                    {
                        Name = "Mahi Mahi",
                        Length = 5.4,
                        Width = 20.75,
                        Thickness = 2.3,
                        Volume = 29.39,
                        Type = "Fish",
                        Price = 645,
                        Equipment = null,
                        PicturePath = "https://www.thesurfboardwarehouse.com.au/cdn/shop/products/Mahi_Mahi_Teal_3up_800x.jpg?v=1668486120"
                    },
                    new Board
                    {
                        Name = "The Emerald Glider",
                        Length = 9.2,
                        Width = 22.8,
                        Thickness = 2.8,
                        Volume = 65.4,
                        Type = "Longboard",
                        Price = 895,
                        Equipment = null,
                        PicturePath = null
                    },
                    new Board
                    {
                        Name = "Naish Maliko",
                        Length = 14,
                        Width = 25,
                        Thickness = 6,
                        Volume = 330,
                        Type = "SUP",
                        Price = 1304,
                        Equipment = "Fin, Paddle, Pump, Leash",
                        PicturePath = null
                    },
                    new Board
                    {
                        Name = "Six Tourer",
                        Length = 11.6,
                        Width = 32,
                        Thickness = 6,
                        Volume = 270,
                        Type = "SUB",
                        Price = 611,
                        Equipment = "Fin, Paddle, Pump, Leash",
                        PicturePath = null
                    },
                    new Board
                    {
                        Name = "Naish One",
                        Length = 12.6,
                        Width = 30,
                        Thickness = 6,
                        Volume = 301,
                        Type = "SUP",
                        Price = 854,
                        Equipment = "Paddle",
                        PicturePath = null
                    },
                    new Board
                    {
                        Name = "Walden Magic",
                        Length = 9.6,
                        Width = 19.4,
                        Thickness = 3,
                        Volume = 80,
                        Type = "Longboard",
                        Price = 1025,
                        Equipment = null,
                        PicturePath = null
                    },
                    new Board
                    {
                        Name = "The Bomb",
                        Length = 5.5,
                        Width = 21,
                        Thickness = 2.5,
                        Volume = 33.7,
                        Type = "Shortboard",
                        Price = 645,
                        Equipment = null,
                        PicturePath = "https://cdn.pixabay.com/photo/2017/01/31/16/59/bomb-2025548_1280.png"
                    }
                );
                context.SaveChanges();
                Console.WriteLine("[+] Surfboards have been Seeded!");
            }
        }
    }
}
