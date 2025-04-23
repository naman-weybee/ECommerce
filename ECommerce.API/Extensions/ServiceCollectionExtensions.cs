using ECommerce.API.Conventions;
using ECommerce.API.Filters;
using ECommerce.API.Helper;
using ECommerce.API.Helper.Interfaces;
using ECommerce.API.Mappings;
using ECommerce.Application.Interfaces;
using ECommerce.Application.ServiceHelper;
using ECommerce.Application.Services;
using ECommerce.Application.Templates;
using ECommerce.Application.Validators.Base;
using ECommerce.Domain.DomainServices.Interfaces;
using ECommerce.Domain.DomainServices.Services;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Events;
using ECommerce.Domain.ValueObjects.JsonConverters;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Infrastructure.Services;
using ECommerce.Shared.Interfaces;
using ECommerce.Shared.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace ECommerce.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            services.AddScoped<IHTTPHelper, HTTPHelper>();
            services.AddScoped<IControllerHelper, ControllerHelper>();

            services.AddScoped<ExecutionFilter>();

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddEventHandlerServices();

            services.AddLogging();

            services.AddHttpContextAccessor();

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
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleEntityService, RoleEntityService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAccessTokenService, AccessTokenService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IMD5Service, MD5Service>();

            services.AddScoped<IPaginationService, PaginationService>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IOTPService, OTPService>();

            services.AddScoped<IEmailTemplates, EmailTemplates>();

            return services;
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IIProductDomainService, ProductService>();

            services.AddScoped<IRepository<Product>, Repository<Product>>();
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<IRepository<OrderItem>, Repository<OrderItem>>();
            services.AddScoped<IRepository<CartItem>, Repository<CartItem>>();
            services.AddScoped<IRepository<Order>, Repository<Order>>();
            services.AddScoped<IRepository<Address>, Repository<Address>>();
            services.AddScoped<IRepository<Country>, Repository<Country>>();
            services.AddScoped<IRepository<State>, Repository<State>>();
            services.AddScoped<IRepository<City>, Repository<City>>();
            services.AddScoped<IRepository<Gender>, Repository<Gender>>();
            services.AddScoped<IRepository<Role>, Repository<Role>>();
            services.AddScoped<IRepository<RoleEntity>, Repository<RoleEntity>>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<RefreshToken>, Repository<RefreshToken>>();
            services.AddScoped<IRepository<OTP>, Repository<OTP>>();

            services.AddScoped<IServiceHelper<Product>, ServiceHelper<Product>>();
            services.AddScoped<IServiceHelper<Category>, ServiceHelper<Category>>();
            services.AddScoped<IServiceHelper<OrderItem>, ServiceHelper<OrderItem>>();
            services.AddScoped<IServiceHelper<CartItem>, ServiceHelper<CartItem>>();
            services.AddScoped<IServiceHelper<Order>, ServiceHelper<Order>>();
            services.AddScoped<IServiceHelper<Address>, ServiceHelper<Address>>();
            services.AddScoped<IServiceHelper<Country>, ServiceHelper<Country>>();
            services.AddScoped<IServiceHelper<State>, ServiceHelper<State>>();
            services.AddScoped<IServiceHelper<City>, ServiceHelper<City>>();
            services.AddScoped<IServiceHelper<Gender>, ServiceHelper<Gender>>();
            services.AddScoped<IServiceHelper<Role>, ServiceHelper<Role>>();
            services.AddScoped<IServiceHelper<RoleEntity>, ServiceHelper<RoleEntity>>();
            services.AddScoped<IServiceHelper<User>, ServiceHelper<User>>();
            services.AddScoped<IServiceHelper<RefreshToken>, ServiceHelper<RefreshToken>>();
            services.AddScoped<IServiceHelper<OTP>, ServiceHelper<OTP>>();

            services.AddScoped<IDomainEventCollector, DomainEventCollector>();

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
            services.AddScoped<ITransactionManagerService, TransactionManagerService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                    };
                });

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssembly(typeof(BaseDTOValidator).Assembly);

            return services;
        }

        private static IServiceCollection AddEventHandlerServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Application.EventHandlers.EventHandler<BaseEvent>).Assembly));

            return services;
        }
    }
}