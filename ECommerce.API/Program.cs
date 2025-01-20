using ECommerce.API.Extensions;
using ECommerce.API.Helper;
using ECommerce.API.Middlewares;

namespace ECommerce.API
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddAPIServices()
                .AddApplicationServices()
                .AddDomainServices()
                .AddInfrastructureServices(builder.Configuration);

            builder.Services.AddFluentValidationServices();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            HTTPHelper.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.Run();
        }
    }
}