using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SurfboardApi.Data;
using SurfboardApi.Hubs;

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
            builder.Services.AddApiVersioning(Options =>
            {
                Options.AssumeDefaultVersionWhenUnspecified = true;
                Options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                Options.ReportApiVersions = true;
                Options.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("x-version"));
            });

            builder.Services.AddVersionedApiExplorer(Options =>
            {
                Options.GroupNameFormat = "'v'VVV";
                Options.SubstituteApiVersionInUrl = true;
            });

            builder.Services.AddSignalR();
            builder.Services.AddResponseCompression(opts =>
                {
                    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                        new[] { "application/octet-stream" }
                        );
                });

            var app = builder.Build();

            app.UseResponseCompression();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(Options =>
            {
                Options.AllowAnyOrigin();
                Options.AllowAnyMethod();
                Options.AllowAnyHeader();
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.MapHub<LiveChatHub>("/live-chat");
            app.MapFallbackToFile("index.html");
            app.Run();
        }
    }
}