using ECommerce.API.Conventions;
using ECommerce.API.Filters;
using ECommerce.API.Helper;
using ECommerce.API.Mappings;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Services;
using ECommerce.Application.Templates;
using ECommerce.Application.Validators;
using ECommerce.Domain.Aggregates;
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

            services.AddScoped<IRepository<ProductAggregate, Product>, Repository<ProductAggregate, Product>>();
            services.AddScoped<IRepository<CategoryAggregate, Category>, Repository<CategoryAggregate, Category>>();
            services.AddScoped<IRepository<OrderItemAggregate, OrderItem>, Repository<OrderItemAggregate, OrderItem>>();
            services.AddScoped<IRepository<CartItemAggregate, CartItem>, Repository<CartItemAggregate, CartItem>>();
            services.AddScoped<IRepository<OrderAggregate, Order>, Repository<OrderAggregate, Order>>();
            services.AddScoped<IRepository<AddressAggregate, Address>, Repository<AddressAggregate, Address>>();
            services.AddScoped<IRepository<CountryAggregate, Country>, Repository<CountryAggregate, Country>>();
            services.AddScoped<IRepository<StateAggregate, State>, Repository<StateAggregate, State>>();
            services.AddScoped<IRepository<CityAggregate, City>, Repository<CityAggregate, City>>();
            services.AddScoped<IRepository<GenderAggregate, Gender>, Repository<GenderAggregate, Gender>>();
            services.AddScoped<IRepository<RoleAggregate, Role>, Repository<RoleAggregate, Role>>();
            services.AddScoped<IRepository<RoleEntityAggregate, RoleEntity>, Repository<RoleEntityAggregate, RoleEntity>>();
            services.AddScoped<IRepository<UserAggregate, User>, Repository<UserAggregate, User>>();
            services.AddScoped<IRepository<RefreshTokenAggregate, RefreshToken>, Repository<RefreshTokenAggregate, RefreshToken>>();
            services.AddScoped<IRepository<OTPAggregate, OTP>, Repository<OTPAggregate, OTP>>();

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