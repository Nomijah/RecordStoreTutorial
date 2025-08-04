using Microsoft.EntityFrameworkCore;
using RecordStore.Core.Interfaces;
using RecordStore.Infrastructure.Data;
using RecordStore.Infrastructure.Repositories;
using RecordStore.Services.Interfaces;
using RecordStore.Services.Services;

namespace RecordStore.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "Record Store API", Version = "v1" });
            });

            // Database
            builder.Services.AddDbContext<RecordStoreDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Repository pattern
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Service pattern
            builder.Services.AddScoped<IAlbumService, AlbumService>();

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Record Store API V1");
                });
            //}

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthorization();
            app.MapControllers();

            // Ensure database is created
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<RecordStoreDbContext>();
                context.Database.EnsureCreated();
            }

            app.Run();
        }
    }
}
