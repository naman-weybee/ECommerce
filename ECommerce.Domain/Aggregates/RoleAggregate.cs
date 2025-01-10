using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Infrastructure.Services;

namespace ECommerce.Domain.Aggregates
{
    public class RoleAggregate : AggregateRoot<Role>
    {
        private readonly IDomainEventCollector _eventCollector;

        public Role Role { get; set; }

        public RoleAggregate(Role entity, IDomainEventCollector eventCollector)
            : base(entity, eventCollector)
        {
            Role = entity;
            _eventCollector = eventCollector;
        }

        public void CreateRole(Role role)
        {
            Role.CreateRole(role.Name);

            EventType = eEventType.RoleCreated;
            RaiseDomainEvent();
        }

        public void UpdateRole(Role role)
        {
            Role.UpdateRole(role.Id, role.Name);

            EventType = eEventType.RoleUpdated;
            RaiseDomainEvent();
        }

        public void DeleteRole()
        {
            EventType = eEventType.RoleDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new RoleEvent(Role.Id, Role.Name, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}