using Microsoft.EntityFrameworkCore;
using SurfboardApi.Data;

namespace SurfboardApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<SurfBoardApiContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("SurfBoardApiContext") ?? throw new InvalidOperationException("Connection string 'SurfBoardApiContext' not found.")));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            HttpClient httpClient = new();
            builder.Services.AddSingleton(typeof(HttpClient), httpClient);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}