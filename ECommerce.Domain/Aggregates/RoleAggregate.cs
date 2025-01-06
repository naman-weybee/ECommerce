using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;

namespace ECommerce.Domain.Aggregates
{
    public class RoleAggregate : AggregateRoot<Role>
    {
        public Role Role { get; set; }

        public RoleAggregate(Role entity)
            : base(entity)
        {
            Role = entity;
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
            var domainEvent = new RoleEvent(Role.Id, EventType);
            RaiseDomainEvent(domainEvent);
        }

        public new void ClearDomainEvents()
        {
            base.ClearDomainEvents();
        }
    }
}