using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurfboardApi.Models;

namespace SurfboardApi.Data
{
    public class SurfBoardApiContext : DbContext
    {
        public SurfBoardApiContext (DbContextOptions<SurfBoardApiContext> options)
            : base(options)
        {
        }

        public DbSet<SurfboardApi.Models.Board> Board { get; set; } = default!;
        public DbSet<SurfboardApi.Models.Bookings> Bookings { get; set; }
    }
}
