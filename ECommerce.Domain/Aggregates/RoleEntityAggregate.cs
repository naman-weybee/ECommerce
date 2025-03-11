using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Services;

namespace ECommerce.Domain.Aggregates
{
    public class RoleEntityAggregate : AggregateRoot<RoleEntity>
    {
        private readonly IDomainEventCollector _eventCollector;

        public RoleEntity RoleEntity { get; set; }

        public RoleEntityAggregate(RoleEntity entity, IDomainEventCollector eventCollector)
            : base(entity, eventCollector)
        {
            RoleEntity = entity;
            _eventCollector = eventCollector;
        }
    }
}