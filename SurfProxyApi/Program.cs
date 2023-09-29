using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;
using SurfProxyApi.Data;
using SurfProxyApi.Seeders;

namespace SurfProxyApi
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<SurfBoardWebContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SurfBoardWebContext") ?? throw new InvalidOperationException("Connection string 'SurfBoardWebContext' not found.")));


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                SeedSurfboards.Initialize(services);
            }

            app.UseHttpsRedirection();



            app.MapControllers();

            app.Run();
        }
    }
}