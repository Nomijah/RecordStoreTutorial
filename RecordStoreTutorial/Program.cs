using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecordStore.Core.Interfaces;
using RecordStore.Infrastructure.Data;
using RecordStore.Infrastructure.Repositories;
using RecordStore.Services.DTOs;
using RecordStore.Services.Interfaces;
using RecordStore.Services.Mapping;
using RecordStore.Services.Services;
using RecordStore.Services.Validators;
using RecordStore.WebAPI.MiddleWare;
using System.Reflection;

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
                c.SwaggerDoc("v1", new()
                {
                    Title = "Record Store API",
                    Version = "v1",
                    Description = "A comprehensive API for managing a record store with albums, artists, and genres"
                });

                // Include XML comments for better Swagger documentation
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });

            // Database
            builder.Services.AddDbContext<RecordStoreDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Repository pattern
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Service pattern
            builder.Services.AddScoped<IAlbumService, AlbumService>();
            builder.Services.AddScoped<IArtistService, ArtistService>();

            // FluentValidation
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateAlbumDtoValidator>();
            
            // AutoMapper
            builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

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

            //Configure the HTTP request pipeline.
            app.UseMiddleware<GlobalExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Record Store API V1");
                });
            }

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
