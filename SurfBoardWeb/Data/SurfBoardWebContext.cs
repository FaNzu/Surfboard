using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurfBoardWeb.Models;

namespace SurfBoardWeb.Data
{
    public class SurfBoardWebContext : IdentityDbContext<DefaultUser>
    {
        public SurfBoardWebContext(DbContextOptions<SurfBoardWebContext> options)
            : base(options)
        {
        }

        public DbSet<SurfBoardWeb.Models.Board> Board { get; set; } = default!;


    }
}
