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
            if (role.HasFullPermission)
                SetFullPermission(role);

            Role.CreateRole(role.Name, role.RoleEntity, role.HasViewPermission, role.HasCreateOrUpdatePermission, role.HasDeletePermission, role.HasFullPermission);

            EventType = eEventType.RoleCreated;
            RaiseDomainEvent();
        }

        public void UpdateRole(Role role)
        {
            if (role.HasFullPermission)
                SetFullPermission(role);

            Role.UpdateRole(role.Id, role.Name, role.RoleEntity, role.HasViewPermission, role.HasCreateOrUpdatePermission, role.HasDeletePermission, role.HasFullPermission);

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

        private void SetFullPermission(Role role)
        {
            role.RoleEntity = eRoleEntity.Full;
            role.HasViewPermission = true;
            role.HasCreateOrUpdatePermission = true;
            role.HasDeletePermission = true;
        }
    }
}