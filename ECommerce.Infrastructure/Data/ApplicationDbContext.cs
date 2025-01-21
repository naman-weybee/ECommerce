using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data.Configurations;
using ECommerce.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;
using System.Linq.Expressions;

namespace ECommerce.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDomainEventCollector _eventCollector;

        public ApplicationDbContext()
        : base()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDomainEventCollector eventCollector)
        : base(options)
        {
            _eventCollector = eventCollector;
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Address> Address { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Gender> Gender { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var markedAsDeleted = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Deleted);

            foreach (var item in markedAsDeleted)
            {
                if (item.Entity is Base baseDto)
                {
                    item.State = EntityState.Unchanged;
                    baseDto.IsDeleted = true;
                    baseDto.DeletedDate = DateTime.UtcNow;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);
            await PublishDomainEventsAsync();

            return result;
        }

        private async Task PublishDomainEventsAsync()
        {
            await _eventCollector.PublishAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(Base).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType);
                    var deletedCheck = Expression.Lambda(Expression.Equal(Expression.Property(parameter, "IsDeleted"), Expression.Constant(false)), parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(deletedCheck);
                }
            }

            modelBuilder
            .ApplyConfiguration(new ProductConfiguration())
            .ApplyConfiguration(new CategoryConfiguration())
            .ApplyConfiguration(new OrderItemConfiguration())
            .ApplyConfiguration(new CartItemConfiguration())
            .ApplyConfiguration(new OrderConfiguration())
            .ApplyConfiguration(new AddressConfiguration())
            .ApplyConfiguration(new CountryConfiguration())
            .ApplyConfiguration(new StateConfiguration())
            .ApplyConfiguration(new CityConfiguration())
            .ApplyConfiguration(new GenderConfiguration())
            .ApplyConfiguration(new RoleConfiguration())
            .ApplyConfiguration(new UserConfiguration())
            .ApplyConfiguration(new RefreshTokenConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}