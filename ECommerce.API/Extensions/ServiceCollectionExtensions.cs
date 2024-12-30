using ECommerce.API.Mappings;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Services;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Domain.ValueObjects.JsonConverters;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Shared.Interfaces;
using ECommerce.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IPaginationService, PaginationService>();

            return services;
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository<ProductAggregate, Product>, Repository<ProductAggregate, Product>>();
            services.AddScoped<IRepository<CategoryAggregate, Category>, Repository<CategoryAggregate, Category>>();
            services.AddScoped<IRepository<OrderAggregate, Order>, Repository<OrderAggregate, Order>>();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new MoneyJsonConverter());
                options.SerializerSettings.Converters.Add(new CurrencyJsonConverter());
            });

            return services;
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}