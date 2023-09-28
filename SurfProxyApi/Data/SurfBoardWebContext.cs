using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurfProxyApi.Models;

namespace SurfProxyApi.Data
{
    public class SurfBoardWebContext : IdentityDbContext<DefaultUser>
    {
        public SurfBoardWebContext (DbContextOptions<SurfBoardWebContext> options)
            : base(options)
        {
        }

        public DbSet<Board> Board { get; set; } = default!;
        public DbSet<Bookings> Bookings { get; set; }
    }
}
