using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Infrastructure.Services;

namespace ECommerce.Domain.Aggregates
{
    public class RolePermissionAggregate : AggregateRoot<RolePermission>
    {
        private readonly IDomainEventCollector _eventCollector;

        public RolePermission RolePermission { get; set; }

        public RolePermissionAggregate(RolePermission entity, IDomainEventCollector eventCollector)
            : base(entity, eventCollector)
        {
            RolePermission = entity;
            _eventCollector = eventCollector;
        }

        public void CreateRolePermission()
        {
            Entity.CreateRolePermission(Entity.RoleId, Entity.RoleEntityId, Entity.HasViewPermission, Entity.HasCreateOrUpdatePermission, Entity.HasDeletePermission, Entity.HasFullPermission);

            EventType = eEventType.RolePermissionCreated;
            RaiseDomainEvent();
        }

        public void UpdateRolePermission()
        {
            Entity.UpdateRolePermission(Entity.RoleId, Entity.RoleEntityId, Entity.HasViewPermission, Entity.HasCreateOrUpdatePermission, Entity.HasDeletePermission, Entity.HasFullPermission);

            EventType = eEventType.RolePermissionUpdated;
            RaiseDomainEvent();
        }

        public void DeleteRolePermission()
        {
            EventType = eEventType.RolePermissionDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new RolePermissionEvent(Entity.RoleId, Entity.RoleEntityId, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}