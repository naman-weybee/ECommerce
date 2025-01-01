using ECommerce.API.Filters;
using ECommerce.API.Mappings;
using ECommerce.API.Validators;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Services;
using ECommerce.Application.Validators;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Domain.ValueObjects.JsonConverters;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Shared.Interfaces;
using ECommerce.Shared.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            services.AddScoped<ExecutionFilter>();

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

        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssemblyContaining<ProductDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<ProductCreateDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<ProductUpdateDTOValidator>();

            services.AddValidatorsFromAssemblyContaining<CategoryDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<CategoryCreateDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<CategoryUpdateDTOValidator>();

            services.AddValidatorsFromAssemblyContaining<OrderDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<OrderCreateDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<OrderUpdateDTOValidator>();

            services.AddValidatorsFromAssemblyContaining<OrderItemDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<OrderItemCreateDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<OrderItemUpdateDTOValidator>();

            services.AddValidatorsFromAssemblyContaining<AddressDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<AddressCreateDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<AddressUpdateDTOValidator>();

            services.AddValidatorsFromAssemblyContaining<UserDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<UserCreateDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<UserUpdateDTOValidator>();

            return services;
        }
    }
}