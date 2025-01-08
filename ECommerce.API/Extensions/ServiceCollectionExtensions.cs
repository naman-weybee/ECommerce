﻿using ECommerce.API.Conventions;
using ECommerce.API.Filters;
using ECommerce.API.Mappings;
using ECommerce.API.Validators;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Services;
using ECommerce.Application.Validators;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.DomainInterfaces;
using ECommerce.Domain.DomainServices;
using ECommerce.Domain.Entities;
using ECommerce.Domain.ValueObjects.JsonConverters;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Shared.Interfaces;
using ECommerce.Shared.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<ICartItemService, CartItemService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IPaginationService, PaginationService>();

            return services;
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IIProductDomainService, ProductService>();

            services.AddScoped<IRepository<ProductAggregate, Product>, Repository<ProductAggregate, Product>>();
            services.AddScoped<IRepository<CategoryAggregate, Category>, Repository<CategoryAggregate, Category>>();
            services.AddScoped<IRepository<OrderItemAggregate, OrderItem>, Repository<OrderItemAggregate, OrderItem>>();
            services.AddScoped<IRepository<CartItemAggregate, CartItem>, Repository<CartItemAggregate, CartItem>>();
            services.AddScoped<IRepository<OrderAggregate, Order>, Repository<OrderAggregate, Order>>();
            services.AddScoped<IRepository<AddressAggregate, Address>, Repository<AddressAggregate, Address>>();
            services.AddScoped<IRepository<GenderAggregate, Gender>, Repository<GenderAggregate, Gender>>();
            services.AddScoped<IRepository<RoleAggregate, Role>, Repository<RoleAggregate, Role>>();
            services.AddScoped<IRepository<UserAggregate, User>, Repository<UserAggregate, User>>();

            services.AddControllers(options =>
            {
                options.Filters.Add(new ApiControllerAttribute());
                options.Conventions.Add(new RoutePrefixConvention("api/v1"));
                options.Filters.Add<ExecutionFilter>();
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
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
            services.AddValidatorsFromAssemblyContaining<ProductStockChangeDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<ProductPriceChangeDTOValidator>();

            services.AddValidatorsFromAssemblyContaining<CategoryDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<CategoryCreateDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<CategoryUpdateDTOValidator>();

            services.AddValidatorsFromAssemblyContaining<OrderItemDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<OrderItemCreateDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<OrderItemUpdateDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<OrderItemQuantityUpdateDTOValidator>();

            services.AddValidatorsFromAssemblyContaining<CartItemDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<CartItemCreateDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<CartItemUpdateDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<CartItemQuantityUpdateDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<CartItemUnitPriceUpdateDTOValidator>();

            services.AddValidatorsFromAssemblyContaining<OrderDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<OrderCreateDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<OrderUpdateDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<OrderUpdateStatusDTOValidator>();

            services.AddValidatorsFromAssemblyContaining<AddressDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<AddressCreateDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<AddressUpdateDTOValidator>();

            services.AddValidatorsFromAssemblyContaining<GenderDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<GenderCreateDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<GenderUpdateDTOValidator>();

            services.AddValidatorsFromAssemblyContaining<RoleDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<RoleCreateDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<RoleUpdateDTOValidator>();

            services.AddValidatorsFromAssemblyContaining<UserDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<UserCreateDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<UserUpdateDTOValidator>();

            return services;
        }
    }
}