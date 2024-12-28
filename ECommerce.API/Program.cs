using ECommerce.API.Mappings;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Services;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Shared.Interfaces;
using ECommerce.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace ECommerce.API
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddControllers().AddNewtonsoftJson();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddScoped<IPaginationService, PaginationService>();

            builder.Services.AddScoped<IRepository<ProductAggregate, Product>, Repository<ProductAggregate, Product>>();
            builder.Services.AddScoped<IRepository<CategoryAggregate, Category>, Repository<CategoryAggregate, Category>>();
            builder.Services.AddScoped<IRepository<OrderAggregate, Order>, Repository<OrderAggregate, Order>>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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